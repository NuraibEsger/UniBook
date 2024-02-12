using System.ComponentModel.DataAnnotations;

namespace UniBook.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string? Surname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Password is wrong")]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string? ConfirmPassword { get; set; }
    }
}
