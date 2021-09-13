using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstitutionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Institution> GetAsync(int id, CancellationToken cancellationToken)
        {
            return _unitOfWork.Institutions.GetByIdAsync(id, cancellationToken);
        }

        public Task<IReadOnlyCollection<Institution>> GetByStructureIdAsync(int structureId, CancellationToken cancellationToken)
        {
            return _unitOfWork.Institutions.GetByStructureIdAsync(structureId, cancellationToken);
        }

        public async Task AddAsync(Institution entity)
        {
            await _unitOfWork.Institutions.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Institution entity)
        {
            _unitOfWork.Institutions.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task UpdateAsync(Institution institution)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<Institution> GetAsync(int id) => await _unitOfWork.Institutions.GetByIdAsync(id);

        public async Task<IEnumerable<Institution>> ListAsync() => await _unitOfWork.Institutions.ListAsync();
    }
}