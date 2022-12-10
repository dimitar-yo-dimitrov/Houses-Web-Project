using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Houses.Infrastructure.Data.Configuration
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            //builder.HasOne(p => p.Owner)
            //    .WithMany(u => u.ApplicationUserProperties)
            //    .HasForeignKey(c => c.OwnerId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
