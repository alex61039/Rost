using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface IUserService
    {
        Task UpdateAsync(ApplicationUser user);
        Task<ApplicationUser> GetAsync(string id);
        Task<int> GetNextPersonalNumber();
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task<ApplicationUser> GetByNumberAsync(string number);
        Task<IList<ApplicationUser>> ListByInstitutionId(int institutionId);
    }
}