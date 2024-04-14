using Microsoft.EntityFrameworkCore;
using Shortify.Data.Configurations;
using Shortify.Data.Entities;
using System.Transactions;

namespace Shortify.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LinkConfiguration());
        }
    }
}
