﻿using System.ComponentModel.DataAnnotations;
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
            ApplicationUserProperties = new HashSet<ApplicationUserProperty>();
            Images = new HashSet<Image>();
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

        public bool IsActive { set; get; } = true;

        public string? ImageUrlId { get; set; }

        [ForeignKey(nameof(ImageUrlId))]
        public virtual Image? ImageUrl { get; set; }

        [ForeignKey(nameof(PropertyType))]
        public string PropertyTypeId { get; set; } = null!;
        public virtual PropertyType PropertyType { get; set; } = null!;

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; } = null!;
        public virtual ApplicationUser Owner { get; set; } = null!;

        [ForeignKey(nameof(City))]
        public string CityId { get; set; } = null!;
        public virtual City City { get; set; } = null!;

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ApplicationUserProperty> ApplicationUserProperties { get; set; }
    }
}
