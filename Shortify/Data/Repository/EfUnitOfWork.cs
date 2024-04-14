using Shortify.Data.Abstractions;
using Shortify.Data.Entities;
using System;

namespace Shortify.Data.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IRepository<Link>> _linkRepository;

        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;

            _linkRepository = new Lazy<IRepository<Link>>(()
                => new LinkRepository(context));
        }

        public IRepository<Link> LinkRepository => _linkRepository.Value;

        public async Task CreateDatabaseAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);
        }

        public async Task RemoveDatabaseAsync(CancellationToken cancellationToken = default)
        {
            await _context.Database.EnsureDeletedAsync(cancellationToken);
        }

        public async Task SaveAllAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
