using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shortify.Data.Entities;

namespace Shortify.Data.Configurations
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LongURL)
                .IsRequired();

            builder.Property(l => l.Created)
                .IsRequired();

            builder.Property(l => l.ClickCount)
                .IsRequired();
        }
    }
}
