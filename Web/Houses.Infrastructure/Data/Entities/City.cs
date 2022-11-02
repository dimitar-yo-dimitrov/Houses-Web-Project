using System.ComponentModel.DataAnnotations;
using static Houses.Infrastructure.Constants.ValidationConstants.City;

namespace Houses.Infrastructure.Data.Entities
{
    public class City
    {
        public City()
        {
            Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [MaxLength(CityMaxName)]
        public string Name { get; set; } = null!;

    }
}
