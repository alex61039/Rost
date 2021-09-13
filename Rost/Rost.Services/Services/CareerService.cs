using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class CareerService : ICareerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CareerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Career career)
        {
            await _unitOfWork.Careers.AddAsync(career);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Career career)
        {
            _unitOfWork.Careers.Remove(career);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Career career)
        {
            await _unitOfWork.CommitAsync();
        }
        
        public async Task<Career> GetAsync(int id) => await _unitOfWork.Careers.GetByIdAsync(id);

        public async Task<IEnumerable<Career>> ListAsync() => await _unitOfWork.Careers.ListAsync();

        public async Task<IEnumerable<Career>> ListByDirectionId(int directionId) =>
            await _unitOfWork.Careers.ListByDirectionId(directionId);
    }
}