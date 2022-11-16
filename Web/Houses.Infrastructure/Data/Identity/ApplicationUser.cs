using System.ComponentModel.DataAnnotations;
using Houses.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using static Houses.Infrastructure.GlobalConstants.ValidationConstants.Property;
using static Houses.Infrastructure.GlobalConstants.ValidationConstants.User;

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
        public string? FirstName { get; set; }

        [StringLength(UserLastNameMaxLength)]
        public string? LastName { get; set; }

        [MaxLength(MaxUrl)]
        public string? ProfilePicture { get; set; }

        public virtual ICollection<ApplicationUserProperty> ApplicationUserProperties { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
