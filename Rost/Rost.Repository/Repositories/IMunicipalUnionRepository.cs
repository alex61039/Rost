using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface IMunicipalUnionRepository : IRepository<MunicipalUnion>
    {
        Task<IEnumerable<MunicipalUnion>> ListByDistrictId(int districtId);
        Task<MunicipalUnion> GetByName(string name);
    }
}