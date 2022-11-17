using System.ComponentModel.DataAnnotations;
using static Houses.Infrastructure.GlobalConstants.ValidationConstants.Property;

namespace Houses.Core.ViewModels.Property
{
    public class PropertyServiceViewModel
    {
        public PropertyServiceViewModel()
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
        public string Price { get; init; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxDescription, MinimumLength = PropertyMinDescription)]
        public string Description { get; init; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(HomeMaxAddress)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; init; } = null!;

        //[Range(typeof(string), SquareMetersMin, SquareMetersMax, ConvertValueInInvariantCulture = true, ParseLimitsInInvariantCulture = true)]
        public string? SquareMeters { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; } = null!;
    }
}
