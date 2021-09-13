using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class MunicipalUnionService : IMunicipalUnionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MunicipalUnionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task AddAsync(MunicipalUnion entity)
        {
            await _unitOfWork.MunicipalUnions.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(MunicipalUnion entity)
        {
            _unitOfWork.MunicipalUnions.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task UpdateAsync(MunicipalUnion municipalUnion)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<MunicipalUnion> GetAsync(int id) => await _unitOfWork.MunicipalUnions.GetByIdAsync(id);

        public async Task<IEnumerable<MunicipalUnion>> ListAsync() => await _unitOfWork.MunicipalUnions.ListAsync();

        public async Task<IEnumerable<MunicipalUnion>> ListByDisrtictId(int districtId) =>
            await _unitOfWork.MunicipalUnions.ListByDistrictId(districtId);

        public async Task<MunicipalUnion> GetByNameAsync(string name) =>
            await _unitOfWork.MunicipalUnions.GetByName(name);
    }
}