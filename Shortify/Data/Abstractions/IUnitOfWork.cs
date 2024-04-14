using Shortify.Data.Entities;

namespace Shortify.Data.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Link> LinkRepository { get; }
        Task RemoveDatabaseAsync(CancellationToken cancellationToken = default);
        Task CreateDatabaseAsync(CancellationToken cancellationToken = default);
        Task SaveAllAsync(CancellationToken cancellationToken = default);
    }
}
