using System.ComponentModel.DataAnnotations;
using static Houses.Common.GlobalConstants.ValidationConstants.Comment;

namespace Houses.Core.ViewModels.Post
{
    public class CreatePostInputViewModel
    {
        public CreatePostInputViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(PostMaxSender, MinimumLength = PostMinSender)]
        public string Sender { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(MassageMax, MinimumLength = MassageMin)]
        public string Content { get; set; } = null!;

        public DateTime? CreatedOn { get; set; }

        public string? AuthorId { get; set; }

        public string? PropertyId { get; set; }
    }
}
