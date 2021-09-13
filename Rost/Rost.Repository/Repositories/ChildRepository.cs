using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class ChildRepository : Repository<Child>, IChildRepository
    {
        public ChildRepository(RostDbContext context) : base(context)
        {
        }

        public override async Task<Child> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
            await Context.Children
                .Include(t => t.Careers)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken: cancellationToken);

        public async Task<IList<Child>> ListByUserId(string userId) =>
            await Context.Children.Where(t => t.UserId == userId).ToListAsync();
        
        public async Task<int> GetLatestUserNumber() => await Context.Children.MaxAsync(t => (int?)t.PersonalNumber) ?? 0;
    }
}