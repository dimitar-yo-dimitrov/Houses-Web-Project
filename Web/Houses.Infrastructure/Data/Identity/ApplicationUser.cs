using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static Houses.Infrastructure.Constants.ValidationConstants.User;

namespace Houses.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(UserFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [StringLength(UserLastNameMaxLength)]
        public string? LastName { get; set; }
    }
}
