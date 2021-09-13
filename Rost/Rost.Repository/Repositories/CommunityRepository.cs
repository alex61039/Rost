using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class CommunityRepository : Repository<Community>, ICommunityRepository
    {
        public CommunityRepository(RostDbContext context) : base(context)
        {
        }

        public async Task<IList<Community>> ListByUserIdAsync(string userId, bool isActive) =>
            await Context.Communities.Where(t => t.UserId == userId && t.IsActive == isActive).ToListAsync();
    }
}