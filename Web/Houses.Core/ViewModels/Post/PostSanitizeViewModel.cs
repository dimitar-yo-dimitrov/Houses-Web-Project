using System.ComponentModel.DataAnnotations;
using static Houses.Common.GlobalConstants.ValidationConstants.Comment;

namespace Houses.Core.ViewModels.Post
{
    public class PostSanitizeViewModel : PostSanitizeViewModelExtended
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(PostMaxSender, MinimumLength = PostMinSender)]
        public string? Sender { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(MassageMax, MinimumLength = MassageMin)]
        public string? Content { get; set; } = null!;
    }
}
