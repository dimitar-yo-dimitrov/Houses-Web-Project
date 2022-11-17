using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Houses.Infrastructure.GlobalConstants.ValidationConstants.Property;

namespace Houses.Infrastructure.Data.Configuration
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder
                .Property(p => p.Description)
                .HasMaxLength(PropertyMaxDescription)
                .IsRequired();

            //builder
            //    .HasOne(p => p.PropertyType)
            //    .WithMany(pt => pt.Properties)
            //    .HasForeignKey(p => p.OwnerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .HasOne(p => p.OwnerId)
            //    .WithMany()
            //    .HasForeignKey(p => p.PropertyTypeId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
