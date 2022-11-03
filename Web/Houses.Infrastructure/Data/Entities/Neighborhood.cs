using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Houses.Infrastructure.Constants.ValidationConstants.Neighborhood;

namespace Houses.Infrastructure.Data.Entities
{
    public class Neighborhood
    {
        public Neighborhood()
        {
            Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(NeighborhoodMaxName)]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(City))]
        public string CityId { get; set; } = null!;
        public virtual City City { get; set; } = null!;
    }
}
