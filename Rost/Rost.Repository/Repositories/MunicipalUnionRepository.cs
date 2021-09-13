using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class MunicipalUnionRepository : Repository<MunicipalUnion>, IMunicipalUnionRepository
    {
        public MunicipalUnionRepository(RostDbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<MunicipalUnion>> ListByDistrictId(int districtId) =>
            await Context.MunicipalUnions.Where(t => t.DistrictId == districtId).ToListAsync();

        public async Task<MunicipalUnion> GetByName(string name) => await Context.MunicipalUnions.FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
    }
}