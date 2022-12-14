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
        [StringLength(SenderMaxLength, MinimumLength = SenderMinLength,
            ErrorMessage = "The field is required!")]
        public string Sender { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(MassageMax, MinimumLength = MassageMin,
            ErrorMessage = "The field {0} must have a minimum length of {2} and a maximum length of {1}!")]
        public string Content { get; set; } = null!;

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public string? AuthorId { get; set; }

        public string? PropertyId { get; set; }
    }
}
