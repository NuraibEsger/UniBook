using System.ComponentModel.DataAnnotations;

namespace UniBook.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string? ConfirmPassword { get; set; }
    }
}
