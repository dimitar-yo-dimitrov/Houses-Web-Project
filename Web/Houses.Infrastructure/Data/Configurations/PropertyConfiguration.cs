using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Houses.Common.GlobalConstants.ValidationConstants.Property;

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
        }
    }
}
