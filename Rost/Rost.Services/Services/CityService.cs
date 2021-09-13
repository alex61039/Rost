using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task AddAsync(City entity)
        {
            await _unitOfWork.Cities.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(City entity)
        {
            _unitOfWork.Cities.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task UpdateAsync(City city)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<City> GetAsync(int id) => await _unitOfWork.Cities.GetByIdAsync(id);

        public async Task<IEnumerable<City>> ListAsync() => await _unitOfWork.Cities.ListAsync();

        public async Task<City> GetByNameAsync(string name) => await _unitOfWork.Cities.GetByName(name);
    }
}