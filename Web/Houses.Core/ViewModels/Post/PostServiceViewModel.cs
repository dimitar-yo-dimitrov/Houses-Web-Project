using System.ComponentModel.DataAnnotations;
using Houses.Core.ViewModels.Property;

namespace Houses.Core.ViewModels.Post
{
    public class PostServiceViewModel : DetailsPropertyServiceModel
    {
        public PostServiceViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; init; }

        public string? Sender { get; set; } = null!;

        public DateTime? Date { get; set; } = DateTime.Now;

        public string? Content { get; set; } = null!;

        public string? CreatedOn { get; set; } = null!;

        public string? AuthorId { get; set; }

        public string? PropertyId { get; set; }

        public IEnumerable<PostServiceViewModel>? Posts { get; set; }
            = new HashSet<PostServiceViewModel>();
    }
}
