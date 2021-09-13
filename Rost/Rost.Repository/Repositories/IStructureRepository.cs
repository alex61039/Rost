using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface IStructureRepository : IRepository<Structure>
    {
        Task<(IReadOnlyCollection<ParentSimpleStructure> CurrentStructureWithParents, IReadOnlyCollection<ChildSimpleStructure> Children)> GetStructureHierarchyAsync(int? currentLevelStructureId,
            CancellationToken cancellationToken);

        Task<bool> ExistsAsync(Expression<Func<Structure, bool>> predicate, CancellationToken cancellationToken);
        Task<Structure> GetWithChildrenAsync(int id, CancellationToken cancellationToken);
        Task<Structure> GetParent(int id, CancellationToken cancellationToken);
    }
}