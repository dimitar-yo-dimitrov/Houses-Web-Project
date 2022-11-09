using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Houses.Infrastructure.Data.Configuration
{
    internal class PropertyTypesConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder
                .HasData(CreatePropertyTypes());
        }

        private static IEnumerable<PropertyType> CreatePropertyTypes()
        {
            var propertyTypes = new List<PropertyType>()
            {
                new()
                {
                    Id = "1",
                    Title = "House"
                },
                new()
                {
                    Id = "2",
                    Title = "Apartment"
                },
                new()
                {
                    Id = "3",
                    Title = "Villa"
                },
                new()
                {
                    Id = "4",
                    Title = "Office"
                },
                new()
                {
                    Id = "5",
                    Title = "Shop"
                },
                new()
                {
                    Id = "6",
                    Title = "Hotel"
                },
            };

            return propertyTypes;
        }
    }
}
