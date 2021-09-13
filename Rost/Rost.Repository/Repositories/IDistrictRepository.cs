using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface IDistrictRepository : IRepository<District>
    {
        Task<IEnumerable<District>> ListByCityId(int cityId);
        Task<District> GetByName(string name);
    }
}