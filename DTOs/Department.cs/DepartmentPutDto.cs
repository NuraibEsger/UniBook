using UniBook.DTOs.Group;

namespace UniBook.DTOs.Department.cs
{
    public class DepartmentPutDto
    {
        public string? Name { get; set; }
        public List<GroupPutDto>? Groups { get; set; }
    }
}
