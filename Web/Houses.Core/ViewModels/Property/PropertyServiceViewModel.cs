using Houses.Infrastructure.Data.Identity;

namespace Houses.Core.ViewModels.Property
{
    public class PropertyServiceViewModel : PropertyPostQueryViewModel
    {
        public PropertyServiceViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; init; }

        public string Title { get; init; } = null!;

        public decimal Price { get; init; }

        public string Description { get; init; } = null!;

        public string Address { get; init; } = null!;

        public double? SquareMeters { get; init; }

        public DateTime? Date { get; set; }

        public string ImageUrl { get; init; } = null!;

        public string? CityId { get; init; } = null!;

        public string? OwnerId { get; set; }

        public string? PropertyTypeId { get; init; } = null!;

        public string PropertyType { get; set; } = null!;

        public ApplicationUser User { get; set; } = null!;
    }
}
