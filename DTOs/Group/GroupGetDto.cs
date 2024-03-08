using UniBook.DTOs.Student;
using UniBook.DTOs.UserGroup;
using UniBook.Entities;

namespace UniBook.DTOs.Group
{
    public class GroupGetDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? DepartmentName { get; set; }
        public List<UserGroupGetDto>? UserGroups { get; set; }
    }
}
