using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface ICareerRepository : IRepository<Career>
    {
        Task<IEnumerable<Career>> ListByDirectionId(int directionId);
    }
}