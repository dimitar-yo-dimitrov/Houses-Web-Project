namespace Houses.Core.ViewModels.Property
{
    public class DetailsPropertyViewModel
    {
        public DetailsPropertyViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; init; }

        public string Title { get; init; } = null!;

        public string CreatedByName { get; set; } = null!;

        public string Address { get; init; } = null!;

        public string ImageUrl { get; init; } = null!;

        public decimal Price { get; init; }

        public string Description { get; init; } = null!;
    }
}
