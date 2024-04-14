using System.Linq.Expressions;

namespace Shortify.Data.Abstractions
{
    public interface IRepository<T>
    {
        Task AddAsync(T item, CancellationToken cancellationToken = default);
        Task UpdateAsync(T item, CancellationToken cancellationToken = default);
        Task DeleteAsync(T item, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAsync(
            CancellationToken cancellationToken = default,
            Expression<Func<T, bool>>? filter = null);
    }
}
