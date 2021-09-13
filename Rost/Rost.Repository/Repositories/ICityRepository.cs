using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<City> GetByName(string name);
    }
}