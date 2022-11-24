using System.ComponentModel.DataAnnotations;
using Ganss.Xss;
using static Houses.Common.GlobalConstants.ValidationConstants.Comment;
using static Houses.Common.GlobalConstants.ValidationConstants.User;

namespace Houses.Core.ViewModels.User
{
    public class PostInputModel
    {
        private readonly IHtmlSanitizer _sanitizer;

        public PostInputModel()
        {
            _sanitizer = new HtmlSanitizer();
            Id = new Guid().ToString();
        }

        public string Id { get; set; }

        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength)]
        public string Title { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [StringLength(MassageMax, MinimumLength = MassageMin)]
        public string Content { get; set; } = null!;

        public string SanitizedContent
            => _sanitizer.Sanitize(Content);

        [Required]
        public string AuthorId { get; set; } = null!;

        public string ReceiverId { get; set; } = null!;
    }
}
