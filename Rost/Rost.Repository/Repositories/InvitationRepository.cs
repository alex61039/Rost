using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Rost.Common.Enums;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class InvitationRepository : Repository<Invitation>, IInvitationRepository
    {
        public InvitationRepository(RostDbContext context) : base(context)
        {
        }

        public async Task<IList<Invitation>> ListByUserId(string userId, bool isActive) =>
            await Context.Invitations
                .Include(t => t.Community)
                .ThenInclude(t => t.Career)
                .Where(t => t.UserId == userId && t.IsActive == isActive).ToListAsync();
    }
}