using System.ComponentModel.DataAnnotations;

namespace UniBook.DTOs.Teacher
{
    public class TeacherSubjectPostDto
    {
        [Required]
        public int SubjectId { get; set; }
    }
}
