using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserDetails(string userId);
        Task<int> GetLatestUserNumber();
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByNumberAsync(int number);
        Task<IList<ApplicationUser>> ListByInstitutionId(int institutionId);
    }
}