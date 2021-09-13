using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DistrictService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task AddAsync(District entity)
        {
            await _unitOfWork.Districts.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(District entity)
        {
            _unitOfWork.Districts.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task UpdateAsync(District entity)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<District> GetAsync(int id) => await _unitOfWork.Districts.GetByIdAsync(id);

        public async Task<IEnumerable<District>> ListAsync() => await _unitOfWork.Districts.ListAsync();

        public async Task<IEnumerable<District>> ListByCityIdAsync(int cityId) =>
            await _unitOfWork.Districts.ListByCityId(cityId);

        public async Task<District> GetByNameAsync(string name) => await _unitOfWork.Districts.GetByName(name);
    }
}