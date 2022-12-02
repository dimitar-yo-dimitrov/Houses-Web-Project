using System.ComponentModel.DataAnnotations;
using static Houses.Common.GlobalConstants.ValidationConstants.Comment;
using static Houses.Common.GlobalConstants.ValidationConstants.User;

namespace Houses.Core.ViewModels.Post
{
    public class PostInputViewModel
    {
        public PostInputViewModel()
        {
            Id = new Guid().ToString();
        }

        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        [Display(Name = "Full Name")]
        public string AuthorName { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required(AllowEmptyStrings = false)]
        [StringLength(MassageMax, MinimumLength = MassageMin)]
        public string Content { get; set; } = null!;

        public string AuthorId { get; set; } = null!;

        public string? ReceiverId { get; set; } = null!;
    }
}
