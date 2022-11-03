using Houses.Infrastructure.Data.Entities;

namespace Houses.Core.ViewModels
{
    public class PropertyViewModel
    {
        public PropertyViewModel()
        {
            Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public string Title { get; set; } = null!;

        public decimal Price { get; set; }

        public string Description { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int? Floor { get; set; }

        public int? SquareMeters { get; set; }

        public bool Elevator { get; set; }

        public Image ImageUrl { get; set; } = null!;

        public string? PropertyType { get; set; }

        public string? City { get; set; } = null!;
    }
}
