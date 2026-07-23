using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Repository;

public class RepositoryBase<TKey, T>(DbContext context) : IRepository<TKey, T> where T : class
{
    public void Add(T entity) => context.Add(entity);
    public ValueTask RemoveAsync(T entity, CancellationToken cancellationToken = default) { context.Remove(entity); return ValueTask.CompletedTask; }
    public Task<T?> GetAsync(TKey id, CancellationToken cancellationToken = default) => context.FindAsync<T>([id!], cancellationToken).AsTask();
    public Task<List<T>> GetAsync(CancellationToken cancellationToken = default) => context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    public Task<bool> ExistAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default) => context.Set<T>().AnyAsync(expression, cancellationToken);
}
