using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Houses.Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;
using static Houses.Infrastructure.Constants.ValidationConstants.Property;

namespace Houses.Infrastructure.Data.Entities
{
    public class Property
    {
        public Property()
        {
            Id = Guid.NewGuid().ToString();
            Images = new HashSet<Image>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(PropertyMaxName)]
        public string Name { get; set; } = null!;

        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(PropertyMaxDescription)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(HomeMaxAddress)]
        //TODO: In DTO [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; set; } = null!;

        [Range(typeof(int), FloorMin, FloorMax)]
        public int? Floor { get; set; }

        [Range(typeof(int), SquareMetersMin, SquareMetersMax)]
        public int? SquareMeters { get; set; }

        public bool Elevator { get; set; }

        [ForeignKey(nameof(PropertyType))]
        public int PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; } = null!;

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;

        public virtual ApplicationUser Owner { get; set; } = null!;

        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;

        [ForeignKey(nameof(Neighborhood))]
        public int NeighborhoodId { get; set; }
        public virtual Neighborhood Neighborhood { get; set; } = null!;

        public virtual ICollection<Image> Images { get; set; }
    }
}
