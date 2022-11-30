using System.ComponentModel.DataAnnotations;
using Houses.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using static Houses.Common.GlobalConstants.ValidationConstants.Property;
using static Houses.Common.GlobalConstants.ValidationConstants.User;

namespace Houses.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ApplicationUserProperties = new HashSet<ApplicationUserProperty>();
            Posts = new HashSet<Post>();
        }

        [StringLength(UserFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [StringLength(UserLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [MaxLength(MaxUrl)]
        public string? ProfilePicture { get; set; }

        public virtual ICollection<ApplicationUserProperty> ApplicationUserProperties { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
