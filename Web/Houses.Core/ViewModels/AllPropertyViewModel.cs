using System.ComponentModel.DataAnnotations;
using Houses.Infrastructure.Data.Entities;
using static Houses.Infrastructure.Constants.ValidationConstants.Property;

namespace Houses.Core.ViewModels
{
    public class AllPropertyViewModel
    {
        [Required]
        [StringLength(PropertyMaxTitle, MinimumLength = PropertyMinTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), PriceMinLength, PriceMaxLength, ConvertValueInInvariantCulture = true)]
        public decimal Price { get; set; }

        [StringLength(PropertyMaxDescription, MinimumLength = PropertyMinDescription)]
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
        [Url]
        public Image ImageUrl { get; set; } = null!;

        public string? PropertyType { get; set; }

        public string? City { get; set; } = null!;
    }
}
