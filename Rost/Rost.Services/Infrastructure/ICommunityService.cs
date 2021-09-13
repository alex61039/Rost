using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface ICommunityService : IBaseService<Community>
    {
        Task<IList<Community>> ListByUserIdAsync(string userId, bool isActive);
    }
}