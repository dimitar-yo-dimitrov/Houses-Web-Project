using System.ComponentModel.DataAnnotations;
using static Houses.Common.GlobalConstants.ValidationConstants.User;

namespace Houses.Core.ViewModels.User
{
    public class EditUserInputViewModel
    {
        public EditUserInputViewModel()
        {
            Id = new Guid().ToString();
        }

        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(UserFirstNameMaxLength, MinimumLength = UserFirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(UserLastNameMaxLength, MinimumLength = UserLastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(RegexPhoneNumber, ErrorMessage = RegexPhoneNumberError)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Url]
        [Display(Name = "Profile Picture")]
        public string? ProfilePicture { get; set; }
    }
}
