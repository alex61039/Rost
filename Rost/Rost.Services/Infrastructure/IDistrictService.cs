using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface IDistrictService : IBaseService<District>
    {
        Task<IEnumerable<District>> ListByCityIdAsync(int cityId);
        Task<District> GetByNameAsync(string name);
    }
}