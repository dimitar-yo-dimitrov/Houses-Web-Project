namespace Houses.Core.ViewModels.Property
{
    public class PropertyServiceViewModel
    {
        public PropertyServiceViewModel()
        {
            Id = new Guid().ToString();
        }

        public string Id { get; init; }

        public string Title { get; init; } = null!;

        public decimal Price { get; init; }

        public string Description { get; init; } = null!;

        public string Address { get; init; } = null!;

        public double? SquareMeters { get; init; }

        public string Date { get; set; } = null!;

        public string ImageUrl { get; init; } = null!;
    }
}
