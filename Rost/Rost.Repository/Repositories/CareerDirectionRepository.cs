using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class CareerDirectionRepository : Repository<CareerDirection>, ICareerDirectionRepository
    {
        public CareerDirectionRepository(RostDbContext context) : base(context)
        {
        }

        public async Task<IList<CareerDirection>> ListWithCareers() =>
            await Context.CareerDirections.Include(t => t.Careers).ToListAsync();
    }
}