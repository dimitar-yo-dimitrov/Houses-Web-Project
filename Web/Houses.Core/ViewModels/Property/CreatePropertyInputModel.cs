using System.ComponentModel.DataAnnotations;
using Houses.Core.ViewModels.City;
using Houses.Core.ViewModels.PropertyType;
using static Houses.Common.GlobalConstants.ValidationConstants.Property;

namespace Houses.Core.ViewModels.Property
{
    public class CreatePropertyInputModel
    {
        public CreatePropertyInputModel()
        {
            Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; init; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxTitle, MinimumLength = PropertyMinTitle)]
        public string Title { get; init; } = null!;

        [Required]
        //[Range(typeof(string), PriceMinLength, PriceMaxLength, ConvertValueInInvariantCulture = true, ParseLimitsInInvariantCulture = true)]
        public decimal Price { get; init; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxDescription, MinimumLength = PropertyMinDescription)]
        public string Description { get; init; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(HomeMaxAddress)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; init; } = null!;

        //[Range(typeof(string), SquareMetersMin, SquareMetersMax, ConvertValueInInvariantCulture = true, ParseLimitsInInvariantCulture = true)]
        public double? SquareMeters { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; } = null!;

        [Display(Name = "City")]
        public string CityId { get; init; } = null!;

        [Display(Name = "Property Type")]
        public string PropertyTypeId { get; init; } = null!;

        public IEnumerable<CityViewModel> Cities { get; set; } = new List<CityViewModel>();

        public IEnumerable<PropertyTypeViewModel> PropertyTypes { get; set; } = new List<PropertyTypeViewModel>();
    }
}
