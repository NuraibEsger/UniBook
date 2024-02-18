using UniBook.DTOs.Student;
using UniBook.Entities;

namespace UniBook.DTOs.Group
{
    public class GroupGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DepartmentName { get; set; }
    }
}
