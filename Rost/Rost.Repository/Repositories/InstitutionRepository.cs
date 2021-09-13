using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class InstitutionRepository : Repository<Institution>, IInstitutionRepository
    {
        public InstitutionRepository(RostDbContext context) : base(context)
        {
        }

        public override async Task<Institution> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await Context.Institutions
                .Include(nameof(Institution.Structure))
                .Include(nameof(Institution.City))
                .Include(nameof(Institution.District))
                .Include(nameof(Institution.MunicipalUnion))
                .Include(t => t.Education)
                .Include(t => t.Employees).ThenInclude(r => r.Photos)
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
            return result;
        }

        public async Task<IReadOnlyCollection<Institution>> GetByStructureIdAsync(int structureId, CancellationToken cancellationToken)
        {
            var result = await Context.Institutions
                .Where(i => i.StructureId == structureId)
                .Include(nameof(Institution.Structure))
                .Include(nameof(Institution.City))
                .Include(nameof(Institution.District))
                .Include(nameof(Institution.MunicipalUnion))
                .Include(nameof(Institution.Education))
                .Include(nameof(Institution.Employees))
                .ToListAsync(cancellationToken);
            return result.AsReadOnly();
        }
    }
}