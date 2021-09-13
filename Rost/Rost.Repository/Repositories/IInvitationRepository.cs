using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface IInvitationRepository : IRepository<Invitation>
    {
        Task<IList<Invitation>> ListByUserId(string userId, bool isActive);
    }
}