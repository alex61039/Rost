using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface IStructureService
    {
        Task<(IReadOnlyCollection<ParentSimpleStructure> CurrentStructureWithParents, IReadOnlyCollection<ChildSimpleStructure> Children)> GetStructureHierarchyAsync(int? currentLevelStructureId,
            CancellationToken cancellationToken);

        Task<Structure> GetAsync(int id, CancellationToken cancellationToken);
        Task<Structure> GetParentAsync(int id, CancellationToken cancellationToken);

        Task AddAsync(Structure structure, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(Expression<Func<Structure, bool>> predicate, CancellationToken cancellationToken);
        Task UpdateAsync(Structure structure, CancellationToken cancellationToken);
        Task DeleteAsync(Structure structure, CancellationToken cancellationToken);
        Task<Structure> GetWithChildrenAsync(int id, CancellationToken cancellationToken);
    }
}