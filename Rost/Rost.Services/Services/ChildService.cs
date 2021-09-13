using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rost.Common.Enums;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class ChildService : IChildService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChildService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task AddAsync(Child entity)
        {
            await _unitOfWork.Children.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Child entity)
        {
            _unitOfWork.Children.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task UpdateAsync(Child child)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<Child> GetAsync(int id) => await _unitOfWork.Children.GetByIdAsync(id);

        public async Task<IEnumerable<Child>> ListAsync() => await _unitOfWork.Children.ListAsync();

        public async Task<IList<Child>> ListByUserId(string userId) => await _unitOfWork.Children.ListByUserId(userId);

        public async Task<bool> IsAlreadyExistsAsync(string userId, string name, Sex sex)
        {
            var children = await ListByUserId(userId);

            if (children == null || !children.Any())
            {
                return false;
            }

            return children.Any(t => t.Name.ToLower() == name.ToLower() && t.Sex == sex);
        }
    }
}