using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface ICommunityRepository : IRepository<Community>
    {
        Task<IList<Community>> ListByUserIdAsync(string userId, bool isActive);
    }
}