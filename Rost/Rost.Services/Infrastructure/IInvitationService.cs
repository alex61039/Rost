using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface IInvitationService : IBaseService<Invitation>
    {
        Task<IEnumerable<Invitation>> ListByUserId(string userId, bool isActive);

        Task Accept(Invitation invitation, int[] children);
    }
}