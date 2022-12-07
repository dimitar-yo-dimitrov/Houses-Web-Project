using System.ComponentModel.DataAnnotations;
using Houses.Infrastructure.Data.Common.Models;
using Houses.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using static Houses.Common.GlobalConstants.ValidationConstants.Property;
using static Houses.Common.GlobalConstants.ValidationConstants.User;

namespace Houses.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            ApplicationUserProperties = new HashSet<ApplicationUserProperty>();
            Posts = new HashSet<Post>();
            Roles = new HashSet<IdentityUserRole<string>>();
        }

        [StringLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [StringLength(UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [MaxLength(MaxUrl)]
        public string? ProfilePicture { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<ApplicationUserProperty> ApplicationUserProperties { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
