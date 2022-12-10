using System.ComponentModel.DataAnnotations.Schema;
using Houses.Infrastructure.Data.Identity;

namespace Houses.Infrastructure.Data.Entities
{
    public class ApplicationUserProperty
    {
        public ApplicationUserProperty()
        {
            ApplicationUserId = Guid.NewGuid().ToString();
            PropertyId = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(ApplicationUser))]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; } = null!;

        [ForeignKey(nameof(Property))]
        public string PropertyId { get; set; }
        public virtual Property Property { get; set; } = null!;
    }
}
