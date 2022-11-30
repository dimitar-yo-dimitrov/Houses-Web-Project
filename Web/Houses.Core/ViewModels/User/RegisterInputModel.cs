using System.ComponentModel.DataAnnotations;
using static Houses.Common.GlobalConstants.ValidationConstants.User;

namespace Houses.Core.ViewModels.User
{
    public class RegisterInputModel
    {
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
        [MaxLength(PhoneNumberMaxLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [Compare(nameof(ConfirmPassword))]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Url]
        public string? ProfilePicture { get; set; }
    }
}
