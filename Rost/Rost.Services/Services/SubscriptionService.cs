using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Common.Enums;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task AddAsync(Subscription entity)
        {
            await _unitOfWork.Subscriptions.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Subscription entity)
        {
            _unitOfWork.Subscriptions.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task UpdateAsync(Subscription subscription)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<Subscription> GetAsync(int id) => await _unitOfWork.Subscriptions.GetByIdAsync(id);

        public async Task<IEnumerable<Subscription>> ListAsync() => await _unitOfWork.Subscriptions.ListAsync();

        public async Task<IList<Subscription>> ListByChildrenAsync(IEnumerable<int> children, SubscriptionStatus status) =>
            await _unitOfWork.Subscriptions.ListByChildren(children, status);

        public async Task<IList<Subscription>> ListByCommunity(int communityId, SubscriptionStatus status) =>
            await _unitOfWork.Subscriptions.ListByCommunity(communityId, status);

        public async Task Unsubscribe(Subscription entity)
        {
            entity.Status = SubscriptionStatus.Declined;
            _unitOfWork.Subscriptions.Update(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}