using UniBook.DTOs.Semester;

namespace UniBook.DTOs.Course
{
    public class CoursePutDto
    {
        public int Number { get; set; }
        public List<SemesterPutDto>? Semesters { get; set; }
    }
}
