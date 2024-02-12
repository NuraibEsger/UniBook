using System.ComponentModel.DataAnnotations;
using UniBook.DTOs.Semester;

namespace UniBook.DTOs.Course
{
    public class CoursePostDto
    {
        [Required]
        public int Number { get; set; }
        public List<SemesterPostDto>? Semesters { get; set; }
    }
}
