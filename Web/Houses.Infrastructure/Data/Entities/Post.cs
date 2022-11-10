using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Houses.Infrastructure.Data.Identity;
using static Houses.Infrastructure.GlobalConstants.ValidationConstants.Comment;

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
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(MassageMax)]
        public string Content { get; set; } = null!;

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
