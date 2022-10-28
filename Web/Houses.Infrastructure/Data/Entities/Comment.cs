using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Houses.Infrastructure.Data.Identity;
using static Houses.Infrastructure.Constants.ValidationConstants.Comment;

namespace Houses.Infrastructure.Data.Entities
{
    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(MassageMax)]
        public string Message { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Properties))]
        public string PropertyId { get; set; } = null!;

        public virtual Property Properties { get; set; } = null!;
    }
}
