using System.ComponentModel.DataAnnotations;
using UniBook.DTOs.Subject;
using UniBook.DTOs.UserGroup;
using UniBook.Entities;

namespace UniBook.DTOs.Teacher
{
    public class TeacherGetDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
        public string? Email { get; set; }
        public string? SubjectName { get; set; }
        public List<UserGroupGetDto>? UserGroups { get; set; }  
    }
}
