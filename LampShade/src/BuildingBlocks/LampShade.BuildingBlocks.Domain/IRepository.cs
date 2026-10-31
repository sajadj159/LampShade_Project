using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_Framework.Domain;

public interface IRepository<in TKey, T> where T : class
{
    void Add(T entity);
    ValueTask RemoveAsync(T entity, CancellationToken cancellationToken = default);
    Task<T?> GetAsync(TKey id, CancellationToken cancellationToken = default);
    Task<List<T>> GetAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
}
