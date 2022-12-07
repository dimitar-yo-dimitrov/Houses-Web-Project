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

        public string Address { get; init; } = null!;

        public string ImageUrl { get; init; } = null!;
    }
}
