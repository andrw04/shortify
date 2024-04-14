using Microsoft.EntityFrameworkCore;
using Shortify.Data.Abstractions;
using Shortify.Data.Entities;
using System.Linq.Expressions;
using System;

namespace Shortify.Data.Repository
{
    public class LinkRepository : IRepository<Link>
    {
        private readonly AppDbContext _context;

        public LinkRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Link item, CancellationToken cancellationToken = default)
        {
            _context.Add(item);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Link item, CancellationToken cancellationToken = default)
        {
            _context.Remove(item);

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Link>> GetAsync(CancellationToken cancellationToken = default, Expression<Func<Link, bool>>? filter = null)
        {
            var query = _context.Set<Link>().AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(Link item, CancellationToken cancellationToken = default)
        {
            _context.Entry(item).State = EntityState.Modified;

            return Task.CompletedTask;
        }
    }
}
