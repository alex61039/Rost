using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommunityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Community entity)
        {
            await _unitOfWork.Communities.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Community entity)
        {
            _unitOfWork.Communities.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task UpdateAsync(Community community)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<Community> GetAsync(int id) => await _unitOfWork.Communities.GetByIdAsync(id);

        public async Task<IEnumerable<Community>> ListAsync() => await _unitOfWork.Communities.ListAsync();

        public async Task<IList<Community>> ListByUserIdAsync(string userId, bool isActive) =>
            await _unitOfWork.Communities.ListByUserIdAsync(userId, isActive);
    }
}