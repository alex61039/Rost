using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class CareerDirectionService : ICareerDirectionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CareerDirectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(CareerDirection entity)
        {
            await _unitOfWork.CareerDirections.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(CareerDirection entity)
        {
            _unitOfWork.CareerDirections.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(CareerDirection careerDirection)
        {
            await _unitOfWork.CommitAsync();
        }
        
        public async Task<CareerDirection> GetAsync(int id) => await _unitOfWork.CareerDirections.GetByIdAsync(id);

        public async Task<IEnumerable<CareerDirection>> ListAsync() => await _unitOfWork.CareerDirections.ListAsync();

        public async Task<IList<CareerDirection>> ListWithCareersAsync() =>
            await _unitOfWork.CareerDirections.ListWithCareers();
    }
}