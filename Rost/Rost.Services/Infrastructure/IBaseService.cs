using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rost.Services.Infrastructure
{
    public interface IBaseService<T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> ListAsync();
        Task UpdateAsync(T entity);
    }
}