using System.ComponentModel.DataAnnotations;
using Houses.Infrastructure.Data.Entities;
using static Houses.Infrastructure.Constants.ValidationConstants.City;
using static Houses.Infrastructure.Constants.ValidationConstants.Neighborhood;
using static Houses.Infrastructure.Constants.ValidationConstants.Property;

namespace Houses.Core.ViewModels
{
    public class AddPropertyViewModel
    {
        [Required(ErrorMessage = "The field {0} is required!")]
        [StringLength(PropertyMaxTitle,
            MinimumLength = PropertyMinTitle,
            ErrorMessage = "The field {0} must have a minimum length of {2} and a maximum length of {1}!")]
        public string Title { get; set; } = null!;

        [Required]
        //[Range(typeof(decimal), PriceMinLength, PriceMaxLength, ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

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
        public string City { get; set; } = null!;

        [Required]
        [StringLength(NeighborhoodMaxName, MinimumLength = NeighborhoodMinName)]
        public string Neighborhood { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        public string OwnerId { get; set; } = null!;

        public string PropertyTypeId { get; set; } = null!;

        public IEnumerable<PropertyType> PropertyTypes { get; set; } = new List<PropertyType>();

        //public string CityId { get; set; } = null!;
        //public IEnumerable<City> Cities { get; set; } = new List<City>();
    }
}
