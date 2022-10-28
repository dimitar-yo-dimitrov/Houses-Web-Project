using System.ComponentModel.DataAnnotations;
using static Houses.Infrastructure.Constants.ValidationConstants.City;

namespace Houses.Infrastructure.Data.Entities
{
    public class City
    {
        public City()
        {
            this.Properties = new HashSet<Property>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(CityMaxName)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Property> Properties { get; set; }
    }
}
