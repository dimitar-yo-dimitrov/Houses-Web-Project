using System.ComponentModel.DataAnnotations.Schema;
using Houses.Infrastructure.Data.Identity;
namespace Houses.Infrastructure.Data.Entities
{
    public class ApplicationUserProperty
    {
        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; } = null!;
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
    }
}
