using System.ComponentModel.DataAnnotations;

namespace Houses.Core.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool KeepLoggedIn { get; set; }
    }
}
