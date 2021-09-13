using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface IInstitutionService : IBaseService<Institution>
    {
        Task<Institution> GetAsync(int id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Institution>> GetByStructureIdAsync(int structureId, CancellationToken cancellationToken);
        Task UpdateAsync(Institution institution);
    }
}