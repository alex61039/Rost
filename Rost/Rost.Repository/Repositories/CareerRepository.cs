using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class CareerRepository : Repository<Career>, ICareerRepository
    {
        public CareerRepository(RostDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Career>> ListByDirectionId(int directionId) =>
            await Context.Careers.Where(t => t.CareerDirectionId == directionId).ToListAsync();
    }
}