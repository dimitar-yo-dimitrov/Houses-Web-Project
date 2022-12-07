using System.ComponentModel.DataAnnotations;
using static Houses.Common.GlobalConstants.ValidationConstants.Comment;

namespace Houses.Core.ViewModels.Post
{
    public class PostInputViewModel
    {
        public PostInputViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PostMaxSender, MinimumLength = PostMinSender)]
        public string Sender { get; init; } = null!;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required(AllowEmptyStrings = false)]
        [StringLength(MassageMax, MinimumLength = MassageMin)]
        public string Content { get; set; } = null!;

        public string AuthorId { get; set; } = null!;
    }
}
