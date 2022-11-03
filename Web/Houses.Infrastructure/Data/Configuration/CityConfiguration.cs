using Houses.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Houses.Infrastructure.Data.Configuration
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(CreatePropertyTypes());
        }

        private static IEnumerable<City> CreatePropertyTypes()
        {
            var propertyTypes = new List<City>()
            {
                new()
                {
                    Id = "1",
                    Name = "Sofia"
                },
                new()
                {
                    Id = "2",
                    Name = "Plovdiv"
                },
                new()
                {
                    Id = "3",
                    Name = "Varna"
                },
                new()
                {
                    Id = "4",
                    Name = "Burgas"
                },
                new()
                {
                    Id = "5",
                    Name = "Stara Zagora"
                },
                new()
                {
                    Id = "6",
                    Name = "Nesebar"
                },
                new()
                {
                    Id = "7",
                    Name = "Bansko"
                },
                new()
                {
                    Id = "8",
                    Name = "Veliko Tarnovo"
                },
                new()
                {
                    Id = "9",
                    Name = "Smolyan"
                },
                new()
                {
                    Id = "10",
                    Name = "Blagoevgrad"
                },
                new()
                {
                    Id = "11",
                    Name = "Ruse"
                },
                new()
                {
                    Id = "12",
                    Name = "Vratza"
                },
                new()
                {
                    Id = "13",
                    Name = "Kuklen"
                },
                new()
                {
                    Id = "14",
                    Name = "Asenovgrad"
                },
                new()
                {
                    Id = "15",
                    Name = "Velingrad"
                },
                new()
                {
                    Id = "16",
                    Name = "Koprivshtitza"
                },
                new()
                {
                    Id = "17",
                    Name = "Gabrovo"
                },
                new()
                {
                    Id = "18",
                    Name = "Sozopol"
                },
            };

            return propertyTypes;
        }
    }
}
