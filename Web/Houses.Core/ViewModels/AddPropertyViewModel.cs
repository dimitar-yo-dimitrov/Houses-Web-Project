using System.ComponentModel.DataAnnotations;
using Houses.Infrastructure.Data.Entities;
using static Houses.Infrastructure.Constants.ValidationConstants.City;
using static Houses.Infrastructure.Constants.ValidationConstants.Neighborhood;
using static Houses.Infrastructure.Constants.ValidationConstants.Property;

namespace Houses.Core.ViewModels
{
    public class AddPropertyViewModel
    {
        public AddPropertyViewModel()
        {
            PropertyTypes = new HashSet<PropertyType>();
        }

        [Required(ErrorMessage = "The field {0} is required!")]
        [StringLength(PropertyMaxTitle,
            MinimumLength = PropertyMaxTitle,
            ErrorMessage = "The field {0} must have a minimum length of {2} and a maximum length of {1}!")]
        public string Title { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), PriceMinLength, PriceMaxLength, ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The field {0} is required!")]
        [StringLength(OwnerMaxLength,
            MinimumLength = OwnerMinLength,
            ErrorMessage = "The field {0} must have a minimum length of {2} and a maximum length of {1}!")]
        public string Owner { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required!")]
        [StringLength(PropertyMaxDescription,
            MinimumLength = PropertyMinDescription,
            ErrorMessage = "The field {0} must have a minimum length of {2} and a maximum length of {1}!")]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(HomeMaxAddress)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; set; } = null!;

        [Range(typeof(int), FloorMin, FloorMax)]
        public int? Floor { get; set; }

        [Range(typeof(int), SquareMetersMin, SquareMetersMax)]
        public int? SquareMeters { get; set; }

        public bool Elevator { get; set; }

        [Required]
        [StringLength(CityMaxName, MinimumLength = CityMinName)]
        public City City { get; set; } = null!;

        [Required]
        [StringLength(NeighborhoodMaxName, MinimumLength = NeighborhoodMinName)]
        public Neighborhood Neighborhood { get; set; } = null!;

        [Required]
        [Url]
        public Image ImageUrl { get; set; } = null!;

        public int PropertyTypeId { get; set; }

        public IEnumerable<PropertyType> PropertyTypes { get; set; }
    }
}
