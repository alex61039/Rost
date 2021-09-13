using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rost.Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly RostDbContext Context;

        public Repository(RostDbContext context)
        {
            Context = context;
        }

        public virtual async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await Context.Set<T>().FindAsync(new object[] { id }, cancellationToken);

        public virtual async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default) => await Context.Set<T>().ToListAsync(cancellationToken);

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => Context.Set<T>().Where(predicate);

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => await Context.Set<T>().SingleOrDefaultAsync(predicate, cancellationToken);

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default) => await Context.Set<T>().AddAsync(entity, cancellationToken);

        public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) => await Context.Set<T>().AddRangeAsync(entities, cancellationToken);

        public virtual void Update(T entity) => Context.Entry(entity).State = EntityState.Modified;

        public virtual void Remove(T entity) => Context.Set<T>().Remove(entity);

        public virtual void RemoveRange(IEnumerable<T> entities) => Context.Set<T>().RemoveRange(entities);
    }
}