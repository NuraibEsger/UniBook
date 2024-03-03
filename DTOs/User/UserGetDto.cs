using System.ComponentModel.DataAnnotations;
using UniBook.DTOs.UserGroup;

namespace UniBook.DTOs.Student
{
    public class UserGetDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
        public string? Email { get; set; }
        public List<UserGroupGetDto>? UserGroups { get; set; }
    }
}
