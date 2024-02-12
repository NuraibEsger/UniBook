using UniBook.DTOs.Group;

namespace UniBook.DTOs.Department.cs
{
    public class DepartmentGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<GroupGetDto>? Groups { get; set; }
    }
}
