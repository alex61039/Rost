using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class StructureService : IStructureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StructureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<(IReadOnlyCollection<ParentSimpleStructure> CurrentStructureWithParents, IReadOnlyCollection<ChildSimpleStructure> Children)> GetStructureHierarchyAsync(
            int? currentLevelStructureId, CancellationToken cancellationToken)
        {
            return _unitOfWork.Structures.GetStructureHierarchyAsync(currentLevelStructureId, cancellationToken);
        }

        public Task<Structure> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _unitOfWork.Structures.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Structure> GetParentAsync(int id, CancellationToken cancellationToken) =>
            await _unitOfWork.Structures.GetParent(id, cancellationToken);

        public Task<Structure> GetWithChildrenAsync(int id, CancellationToken cancellationToken)
        {
            return _unitOfWork.Structures.GetWithChildrenAsync(id, cancellationToken);
        }

        public async Task AddAsync(Structure structure, CancellationToken cancellationToken)
        {
            await _unitOfWork.Structures.AddAsync(structure, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public Task<bool> ExistsAsync(Expression<Func<Structure, bool>> predicate, CancellationToken cancellationToken)
        {
            return _unitOfWork.Structures.ExistsAsync(predicate, cancellationToken);
        }

        public Task UpdateAsync(Structure structure, CancellationToken cancellationToken)
        {
            return _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task DeleteAsync(Structure structure, CancellationToken cancellationToken)
        {
            _unitOfWork.Structures.Remove(structure);
            await _unitOfWork.CommitAsync(cancellationToken);
        }
    }
}