using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class DistrictRepository : Repository<District>, IDistrictRepository
    {
        public DistrictRepository(RostDbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<District>> ListByCityId(int cityId) =>
            await Context.Districts.Where(t => t.CityId == cityId).ToListAsync();

        public async Task<District> GetByName(string name) => await Context.Districts.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
    }
}