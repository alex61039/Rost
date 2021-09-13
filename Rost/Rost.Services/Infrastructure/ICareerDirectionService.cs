using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Repository.Domain;

namespace Rost.Services.Infrastructure
{
    public interface ICareerDirectionService : IBaseService<CareerDirection>
    {
        Task<IList<CareerDirection>> ListWithCareersAsync();
    }
}