using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(RostDbContext context) : base(context)
        {
        }

        public async Task<ApplicationUser> GetUserDetails(string userId) => await Context.Users
            .Include(t => t.City)
            .Include(t => t.District)
            .Include(t => t.Photos)
            .Include(t => t.MunicipalUnion)
            .Include(t => t.Children)
            .FirstOrDefaultAsync(t => t.Id == userId);

        public async Task<int> GetLatestUserNumber() => await Context.Users.MaxAsync(t => (int?)t.PersonalNumber) ?? 0;

        public async Task<ApplicationUser> GetByEmailAsync(string email) =>
            await Context.Users.FirstOrDefaultAsync(t => t.Email == email && t.EmailConfirmed);
        
        public async Task<ApplicationUser> GetByNumberAsync(int number) =>
            await Context.Users.FirstOrDefaultAsync(t => t.PersonalNumber == number && t.EmailConfirmed);

        public async Task<IList<ApplicationUser>> ListByInstitutionId(int institutionId) => await 
            Context.Users
                .Include(t => t.Photos)
                .Where(t => t.InstitutionId.HasValue && t.InstitutionId.Value == institutionId)
                .OrderBy(t => t.EmployeeRole).ThenBy(t => t.Surname).ThenBy(t => t.Name)
                .ToListAsync();
    }
}