using UniBook.DTOs.Student;

namespace UniBook.DTOs.Group
{
    public class GroupPutDto
    {
        public string? Name { get; set; }
        public List<StudentPutDto>? Students { get; set; }
    }
}
