using UniBook.DTOs.Semester;

namespace UniBook.DTOs.Course
{
    public class CourseGetDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public List<SemesterGetDto>? Semesters { get; set; }
    }
}
