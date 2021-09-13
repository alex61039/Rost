using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface IMunicipalUnionService : IBaseService<MunicipalUnion>
    {
        Task<IEnumerable<MunicipalUnion>> ListByDisrtictId(int districtId);
        Task<MunicipalUnion> GetByNameAsync(string name);
    }
}