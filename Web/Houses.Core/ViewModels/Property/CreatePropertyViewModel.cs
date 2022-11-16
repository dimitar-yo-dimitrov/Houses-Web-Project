﻿using System.ComponentModel.DataAnnotations;
using Houses.Core.ViewModels.City;
using Houses.Core.ViewModels.PropertyType;
using static Houses.Infrastructure.GlobalConstants.ValidationConstants.Property;

namespace Houses.Core.ViewModels.Property
{
    public class CreatePropertyViewModel
    {
        public CreatePropertyViewModel()
        {
            Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxTitle, MinimumLength = PropertyMinTitle)]
        public string Title { get; set; } = null!;

        [Required]
        //[Range(typeof(string), PriceMinLength, PriceMaxLength, ConvertValueInInvariantCulture = true, ParseLimitsInInvariantCulture = true)]
        public string Price { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(PropertyMaxDescription, MinimumLength = PropertyMinDescription)]
        public string Description { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [MaxLength(HomeMaxAddress)]
        [RegularExpression(RegexAddress, ErrorMessage = RegexAddressError)]
        public string Address { get; set; } = null!;

        //[Range(typeof(string), SquareMetersMin, SquareMetersMax, ConvertValueInInvariantCulture = true, ParseLimitsInInvariantCulture = true)]
        public string? SquareMeters { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "City")]
        public string CityId { get; set; } = null!;

        [Display(Name = "Property Type")]
        public string PropertyTypeId { get; set; } = null!;

        public IEnumerable<CityViewModel> Cities { get; set; } = new List<CityViewModel>();

        public IEnumerable<PropertyTypeViewModel> PropertyTypes { get; set; } = new List<PropertyTypeViewModel>();
    }
}