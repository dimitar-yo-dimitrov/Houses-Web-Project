using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Houses.Core.ViewModels.User
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [HiddenInput(DisplayValue = false)]
        public string? ReturnUrl { get; set; }
    }
}
