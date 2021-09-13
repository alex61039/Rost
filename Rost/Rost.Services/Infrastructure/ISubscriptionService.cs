using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Common.Enums;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface ISubscriptionService : IBaseService<Subscription>
    {
        Task<IList<Subscription>> ListByChildrenAsync(IEnumerable<int> children, SubscriptionStatus status);

        Task<IList<Subscription>> ListByCommunity(int communityId, SubscriptionStatus status);

        Task Unsubscribe(Subscription entity);
    }
}