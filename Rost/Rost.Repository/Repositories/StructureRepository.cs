using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class StructureRepository : Repository<Structure>, IStructureRepository
    {
        public StructureRepository(RostDbContext context) : base(context)
        {
        }

        public async Task<(IReadOnlyCollection<ParentSimpleStructure> CurrentStructureWithParents, IReadOnlyCollection<ChildSimpleStructure> Children)> GetStructureHierarchyAsync(
            int? currentLevelStructureId,
            CancellationToken cancellationToken)
        {
            const string getStructureHierarchyCommandText = @"
;WITH currentWithAllParents AS (SELECT Id, Name, ParentId, 0 AS Level
                                FROM dbo.Structures s
                                WHERE s.Id=@currentLevelStructureId
                                UNION ALL
                                SELECT s2.Id, s2.Name, s2.ParentId, cwap.Level+1 AS Level
                                FROM dbo.Structures s2
                                     INNER JOIN currentWithAllParents cwap ON cwap.ParentId=s2.Id)
, currentWithAllParentsAndRoot AS (SELECT cwap.Id, cwap.Name, cwap.Level FROM currentWithAllParents cwap
                                   UNION ALL
                                   SELECT NULL AS Id, N'Верхний уровень' AS Name, 2147483647 AS Level)
SELECT cwapr.Id, cwapr.Name
FROM currentWithAllParentsAndRoot AS cwapr
ORDER BY cwapr.Level;

SELECT s.Id, s.Name, CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.Structures AS s2 WHERE s.Id=s2.ParentId) THEN 1 ELSE 0 END AS BIT) AS HasChildren
FROM dbo.Structures AS s
WHERE @currentLevelStructureId IS NULL AND s.ParentId IS NULL OR s.ParentId=@currentLevelStructureId
OPTION(RECOMPILE);";
            var connectionString = Context.Database.GetDbConnection().ConnectionString;
            await using var connection = new SqlConnection(connectionString);
            await using var cmd = new SqlCommand(getStructureHierarchyCommandText, connection) { CommandType = CommandType.Text };
            cmd.Parameters.AddWithValue("@currentLevelStructureId", (object) currentLevelStructureId ?? DBNull.Value);
            await connection.OpenAsync(cancellationToken);
            await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

            var currentStructureWithParents = new List<ParentSimpleStructure>();
            var i = 0;
            while (await reader.ReadAsync(cancellationToken))
            {
                currentStructureWithParents.Add(new ParentSimpleStructure
                {
                    Id = await reader.IsDBNullAsync(nameof(ParentSimpleStructure.Id), cancellationToken)
                        ? null
                        : await reader.GetFieldValueAsync<int?>(nameof(ParentSimpleStructure.Id), cancellationToken),
                    Name = await reader.GetFieldValueAsync<string>(nameof(ParentSimpleStructure.Name), cancellationToken),
                    IsCurrentLevel = i == 0 // текущий уровень запросом выводится первым. 
                });
                i++;
            }

            await reader.NextResultAsync(cancellationToken);

            var children = new List<ChildSimpleStructure>();
            while (await reader.ReadAsync(cancellationToken))
            {
                children.Add(new ChildSimpleStructure
                {
                    Id = await reader.GetFieldValueAsync<int>(nameof(ChildSimpleStructure.Id), cancellationToken),
                    Name = await reader.GetFieldValueAsync<string>(nameof(ChildSimpleStructure.Name), cancellationToken),
                    HasChildren = await reader.GetFieldValueAsync<bool>(nameof(ChildSimpleStructure.HasChildren), cancellationToken)
                });
            }

            return (currentStructureWithParents.AsReadOnly(), children.AsReadOnly());
        }

        public Task<bool> ExistsAsync(Expression<Func<Structure, bool>> predicate, CancellationToken cancellationToken)
        {
            return Context.Structures.AsNoTracking().AnyAsync(predicate, cancellationToken);
        }

        public Task<Structure> GetWithChildrenAsync(int id, CancellationToken cancellationToken)
        {
            return Context.Structures.Include(nameof(Structure.Children)).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Structure> GetParent(int id, CancellationToken cancellationToken) =>
            await Context.Structures.Where(t => t.Id == id).Select(t => t.Parent).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}