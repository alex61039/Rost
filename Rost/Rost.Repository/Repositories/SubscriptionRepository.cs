using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Common.Enums;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(RostDbContext context) : base(context)
        {
        }

        public override async Task<Subscription> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
            await Context.Subscriptions
                .Include(t => t.Community)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);

        public async Task<IList<Subscription>> ListByUserId(string userId) =>
            await Context.Subscriptions
                .Include(t => t.Community)
                .Where(t => t.Community.UserId == userId).ToListAsync();

        public async Task<IList<Subscription>> ListByChildren(IEnumerable<int> children, SubscriptionStatus status) =>
            await Context.Subscriptions
                .Include(t => t.Community)
                .ThenInclude(t => t.Career)
                .Where(t => children.Contains(t.ChildId) && t.Status == status)
                .ToListAsync();

        public async Task<IList<Subscription>> ListByCommunity(int communityId, SubscriptionStatus status) =>
            await Context.Subscriptions
                .Where(t => t.CommunityId == communityId && t.Status == status)
                .ToListAsync();
    }
}