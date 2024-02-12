using UniBook.DTOs.Student;
using UniBook.Entities;

namespace UniBook.DTOs.Group
{
    public class GroupGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<StudentGetDto>? Students { get; set; }
        public List<GroupTeacher>? GroupTeachers { get; set; }
    }
}
