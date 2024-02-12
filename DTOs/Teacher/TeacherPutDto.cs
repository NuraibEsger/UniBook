using System.ComponentModel.DataAnnotations;
using UniBook.DTOs.Subject;

namespace UniBook.DTOs.Teacher
{
    public class TeacherPutDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public SubjectPutDto? Subject { get; set; }
    }
}
