using Houses.Core.ViewModels.Property;

namespace Houses.Core.ViewModels.Post
{
    public class PostServiceViewModel : DetailsPropertyServiceModel
    {
        public PostServiceViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Sender { get; set; } = null!;

        public DateTime? Date { get; set; }

        public string Content { get; set; } = null!;

        public string? AuthorId { get; set; }

        public string? PropertyId { get; set; }
    }
}
