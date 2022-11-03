using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static Houses.Infrastructure.Constants.ValidationConstants.Property;

namespace Houses.Infrastructure.Data.Entities
{
    public class Property
    {
        public Property()
        {
            ApplicationUserProperties = new HashSet<ApplicationUserProperty>();
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(PropertyMaxTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [MaxLength(PropertyMaxDescription)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(HomeMaxAddress)]
        //TODO: In DTO [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; set; } = null!;

        //TODO: In DTO [Range(typeof(int), FloorMin, FloorMax)]
        public int? Floor { get; set; }

        //TODO: In DTO [Range(typeof(int), SquareMetersMin, SquareMetersMax)]
        public int? SquareMeters { get; set; }

        public bool Elevator { get; set; }

        [ForeignKey(nameof(PropertyType))]
        public string PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; } = null!;

        [ForeignKey(nameof(City))]
        public string CityId { get; set; } = null!;
        public virtual City City { get; set; } = null!;

        public string? NeighborhoodId { get; set; }

        [ForeignKey(nameof(NeighborhoodId))]
        public virtual Neighborhood Neighborhood { get; set; } = null!;

        [ForeignKey(nameof(Images))]
        public string ImageId { get; set; }
        public virtual Image Images { get; set; } = null!;

        public virtual ICollection<ApplicationUserProperty> ApplicationUserProperties { get; set; }
    }
}
