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

        //public DetailsPropertyServiceModel? PropertyDetails { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(PostMaxSender, MinimumLength = PostMinSender)]
        public string Sender { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(MassageMax, MinimumLength = MassageMin)]
        public string Content { get; set; } = null!;

        public string? AuthorId { get; set; } = null!;

        public Infrastructure.Data.Entities.Property? PropertyId { get; set; } = null!;
    }
}
