using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface ICareerService : IBaseService<Career>
    {
        Task<IEnumerable<Career>> ListByDirectionId(int directionId);
    }
}