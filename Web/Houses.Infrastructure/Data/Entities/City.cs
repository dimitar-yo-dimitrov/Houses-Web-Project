using System.ComponentModel.DataAnnotations;
using static Houses.Common.GlobalConstants.ValidationConstants.City;

namespace Houses.Infrastructure.Data.Entities
{
    public class City
    {

        [Key]
        public string Id { get; set; } = null!;

        [MaxLength(CityMaxName)]
        public string Name { get; set; } = null!;
    }
}
