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
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; init; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxTitle, MinimumLength = PropertyMinTitle)]
        public string Title { get; init; } = null!;

        [Required]
        [Range(typeof(decimal), PriceMinLength, PriceMaxLength)]
        public decimal Price { get; init; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxDescription, MinimumLength = PropertyMinDescription)]
        public string Description { get; init; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(HomeMaxAddress)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; init; } = null!;

        [Range(typeof(double), SquareMetersMin, SquareMetersMax)]
        public double? SquareMeters { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; } = null!;

        [Display(Name = "City")]
        public string CityId { get; init; } = null!;

        [Display(Name = "Property Type")]
        public string PropertyTypeId { get; init; } = null!;

        public virtual IEnumerable<CityViewModel> Cities { get; set; } = new List<CityViewModel>();

        public virtual IEnumerable<PropertyTypeViewModel> PropertyTypes { get; set; } = new List<PropertyTypeViewModel>();
    }
}
