using System;
using System.Threading;
using System.Threading.Tasks;
using Rost.Repository.Repositories;

namespace Rost.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICareerRepository Careers { get; }
        ICareerDirectionRepository CareerDirections { get; }
        IChildRepository Children { get; }
        ICityRepository Cities { get; }
        ICommunityRepository Communities { get; }
        IDistrictRepository Districts { get; }
        IInvitationRepository Invitations { get; }
        IMunicipalUnionRepository MunicipalUnions { get; }
        IUserRepository Users { get; }
        IStructureRepository Structures { get; }
        ISubscriptionRepository Subscriptions { get; }
        IInstitutionRepository Institutions { get; }

        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}