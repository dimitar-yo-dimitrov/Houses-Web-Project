using System.ComponentModel.DataAnnotations;
using Houses.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using static Houses.Infrastructure.Constants.ValidationConstants.User;

namespace Houses.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Properties = new HashSet<Property>();
        }

        [StringLength(UserFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [StringLength(UserLastNameMaxLength)]
        public string? LastName { get; set; }

        public virtual Image? ProfilePic { get; set; }

        public virtual ICollection<Property> Properties { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public virtual Post? PostId { get; set; }
    }
}
