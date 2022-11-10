using System.ComponentModel.DataAnnotations;
using Houses.Core.ViewModels.City;
using Houses.Core.ViewModels.PropertyType;
using static Houses.Infrastructure.GlobalConstants.ValidationConstants.Property;

namespace Houses.Core.ViewModels.Property
{
    public class EditPropertyViewModel
    {
        public EditPropertyViewModel()
        {
            Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxTitle, MinimumLength = PropertyMinTitle)]
        public string Title { get; set; } = null!;

        [Required]
        public string Price { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxDescription, MinimumLength = PropertyMinDescription)]
        public string Description { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(HomeMaxAddress)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; set; } = null!;

        public string? SquareMeters { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        public string CityId { get; set; } = null!;

        public string PropertyTypeId { get; set; } = null!;

        public IEnumerable<CityViewModel> Cities { get; set; } = new List<CityViewModel>();

        public IEnumerable<PropertyTypeViewModel> PropertyTypes { get; set; } = new List<PropertyTypeViewModel>();
    }
}
