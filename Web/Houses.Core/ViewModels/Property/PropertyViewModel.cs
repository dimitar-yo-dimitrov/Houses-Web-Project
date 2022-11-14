namespace Houses.Core.ViewModels.Property
{
    public class PropertyViewModel
    {
        public PropertyViewModel()
        {
            Id = new Guid().ToString();
        }

        public string Id { get; set; }

        public string Title { get; set; } = null!;

        public string Price { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? SquareMeters { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string City { get; set; } = null!;

        public string PropertyType { get; set; } = null!;

        public string Owner { get; set; } = null!;
    }
}
