using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Common.Enums;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<IList<Subscription>> ListByUserId(string userId);
        Task<IList<Subscription>> ListByChildren(IEnumerable<int> children, SubscriptionStatus status);
        Task<IList<Subscription>> ListByCommunity(int communityId, SubscriptionStatus status);
    }
}