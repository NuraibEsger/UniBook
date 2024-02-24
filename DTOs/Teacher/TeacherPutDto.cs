using System.ComponentModel.DataAnnotations;
using UniBook.DTOs.Subject;

namespace UniBook.DTOs.Teacher
{
    public class TeacherPutDto
    {
        [Required]
        public int SubjectId { get; set; }
    }
}
