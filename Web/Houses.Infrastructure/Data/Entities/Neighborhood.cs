using System.ComponentModel.DataAnnotations;
using static Houses.Infrastructure.Constants.ValidationConstants.Neighborhood;

namespace Houses.Infrastructure.Data.Entities
{
    public class Neighborhood
    {
        public Neighborhood()
        {
            this.Properties = new HashSet<Property>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(NeighborhoodMaxName)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Property> Properties { get; set; }
    }
}
