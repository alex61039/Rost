using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(RostDbContext context) : base(context)
        {
        }
        
        public async Task<City> GetByName(string name) =>
            await Context.Cities.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
    }
}