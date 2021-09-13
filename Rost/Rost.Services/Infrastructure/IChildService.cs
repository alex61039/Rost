using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Common.Enums;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface IChildService : IBaseService<Child>
    {
        Task<IList<Child>> ListByUserId(string userId);
        Task<bool> IsAlreadyExistsAsync(string userId, string name, Sex sex);
    }
}