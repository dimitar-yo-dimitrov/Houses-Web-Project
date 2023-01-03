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
        [StringLength(SenderMaxLength, MinimumLength = SenderMinLength,
            ErrorMessage = "The field is required!")]
        public string Sender { get; init; } = null!;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required(AllowEmptyStrings = false)]
        [StringLength(MassageMax, MinimumLength = MassageMin,
            ErrorMessage = "The field {0} must have a minimum length of {2} and a maximum length of {1}!")]
        public string Content { get; set; } = null!;

        public string AuthorId { get; set; } = null!;

        public string? PropertyId { get; set; } = null!;
    }
}
