using System.Threading;
using System.Threading.Tasks;
using Rost.Repository.Repositories;

namespace Rost.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RostDbContext _context;
        private CareerRepository _careerRepository;
        private CareerDirectionRepository _careerDirectionRepository;
        private ChildRepository _childRepository;
        private CityRepository _cityRepository;
        private CommunityRepository _communityRepository;
        private DistrictRepository _districtRepository;
        private InvitationRepository _invitationRepository;
        private MunicipalUnionRepository _municipalUnionRepository;
        private UserRepository _userRepository;
        private StructureRepository _structureRepository;
        private SubscriptionRepository _subscriptionRepository;
        private InstitutionRepository _institutionRepository;

        public UnitOfWork(RostDbContext context)
        {
            _context = context;
        }

        public ICareerRepository Careers => _careerRepository ??= new CareerRepository(_context);
        public ICareerDirectionRepository CareerDirections => _careerDirectionRepository ??= new CareerDirectionRepository(_context);
        public IChildRepository Children => _childRepository ??= new ChildRepository(_context);
        public ICityRepository Cities => _cityRepository ??= new CityRepository(_context);
        public ICommunityRepository Communities => _communityRepository ??= new CommunityRepository(_context);
        public IDistrictRepository Districts => _districtRepository ??= new DistrictRepository(_context);
        public IInvitationRepository Invitations => _invitationRepository ??= new InvitationRepository(_context);
        public IMunicipalUnionRepository MunicipalUnions => _municipalUnionRepository ??= new MunicipalUnionRepository(_context);
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public IStructureRepository Structures => _structureRepository ??= new StructureRepository(_context);
        public ISubscriptionRepository Subscriptions => _subscriptionRepository ??= new SubscriptionRepository(_context);
        public IInstitutionRepository Institutions => _institutionRepository ??= new InstitutionRepository(_context);

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}