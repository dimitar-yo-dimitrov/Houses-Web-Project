namespace Houses.Core.ViewModels.Property
{
    public class DetailsPropertyViewModel
    {
        public DetailsPropertyViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; init; }

        public string? Title { get; init; }

        public string? CreatedByName { get; set; }

        public string? Address { get; init; }

        public string? ImageUrl { get; init; }

        public decimal? Price { get; init; }

        public string? Description { get; init; }
    }
}
