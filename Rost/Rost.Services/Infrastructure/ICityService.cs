using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface ICityService : IBaseService<City>
    {
        Task<City> GetByNameAsync(string name);
    }
}