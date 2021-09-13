using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Repository.Repositories
{
    public interface ICareerDirectionRepository : IRepository<CareerDirection>
    {
        Task<IList<CareerDirection>> ListWithCareers();
    }
}