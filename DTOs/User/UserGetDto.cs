using System.ComponentModel.DataAnnotations;

namespace UniBook.DTOs.Student
{
    public class UserGetDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
        public string? Email { get; set; }
    }
}
