using UniBook.DTOs.Subject;

namespace UniBook.DTOs.Teacher
{
    public class TeacherGetDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public SubjectGetDto? Subject { get; set; }
    }
}
