using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task UpdateAsync(ApplicationUser user)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<ApplicationUser> GetAsync(string id) => await _unitOfWork.Users.GetUserDetails(id);

        public async Task<int> GetNextPersonalNumber()
        {
            var latestUserNumber = await _unitOfWork.Users.GetLatestUserNumber();
            var latestChildNumber = await _unitOfWork.Children.GetLatestUserNumber();
            var latestNumber = latestUserNumber > latestChildNumber ? latestUserNumber : latestChildNumber;
            return latestNumber + 1;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email) =>
            await _unitOfWork.Users.GetByEmailAsync(email);

        public async Task<ApplicationUser> GetByNumberAsync(string number)
        {
            if (int.TryParse(number, out var personalNumber))
            {
                return await _unitOfWork.Users.GetByNumberAsync(personalNumber);
            }

            return null;
        }

        public async Task<IList<ApplicationUser>> ListByInstitutionId(int institutionId) =>
            await _unitOfWork.Users.ListByInstitutionId(institutionId);
    }
}