using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Houses.Infrastructure.Data.Identity;
using static Houses.Common.GlobalConstants.ValidationConstants.Comment;

namespace Houses.Infrastructure.Data.Entities
{
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid().ToString();
            ApplicationUserProperties = new HashSet<ApplicationUserProperty>();
            Posts = new HashSet<Post>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(PostMaxSender)]
        public string Sender { get; set; } = null!;

        [Required]
        [MaxLength(MassageMax)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; } = null!;
        public virtual ApplicationUser Author { get; set; } = null!;

        [ForeignKey(nameof(Property))]
        public string? PropertyId { get; set; }
        public virtual Property Property { get; set; } = null!;

        public virtual ICollection<ApplicationUserProperty> ApplicationUserProperties { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
