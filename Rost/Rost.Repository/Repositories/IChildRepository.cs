using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface IChildRepository : IRepository<Child>
    {
        Task<IList<Child>> ListByUserId(string userId);
        Task<int> GetLatestUserNumber();
    }
}