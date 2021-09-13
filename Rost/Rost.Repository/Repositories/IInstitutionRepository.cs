using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface IInstitutionRepository : IRepository<Institution>
    {
        Task<IReadOnlyCollection<Institution>> GetByStructureIdAsync(int structureId, CancellationToken cancellationToken);
    }
}