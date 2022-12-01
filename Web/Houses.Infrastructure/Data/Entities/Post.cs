using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Houses.Infrastructure.Data.Identity;
using static Houses.Common.GlobalConstants.ValidationConstants.Comment;
using static Houses.Common.GlobalConstants.ValidationConstants.User;

namespace Houses.Infrastructure.Data.Entities
{
    public class Post
    {
        public Post()
        {
            Id = new Guid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(UserNameMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [MaxLength(MassageMax)]
        public string Content { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; } = true;

        [Required]
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; } = null!;

        public virtual ApplicationUser Author { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; } = null!;

        public virtual Property Property { get; set; } = null!;
    }
}
