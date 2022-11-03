using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Houses.Infrastructure.Data.Configuration
{
    internal class PropertyTypeConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.HasData(CreatePropertyTypes());
        }

        private static IEnumerable<PropertyType> CreatePropertyTypes()
        {
            var propertyTypes = new List<PropertyType>()
            {
                new()
                {
                    Id = "1",
                    Title = "Houses"
                },
                new()
                {
                    Id = "2",
                    Title = "Apartments"
                },
                new()
                {
                    Id = "3",
                    Title = "Villas"
                },
                new()
                {
                    Id = "4",
                    Title = "Offices"
                },
                new()
                {
                    Id = "5",
                    Title = "Shops"
                },
                new()
                {
                    Id = "6",
                    Title = "Hotels"
                },
            };

            return propertyTypes;
        }
    }
}
