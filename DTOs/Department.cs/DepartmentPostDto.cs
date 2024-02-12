using System.ComponentModel.DataAnnotations;
using UniBook.DTOs.Group;

namespace UniBook.DTOs.Department.cs
{
    public class DepartmentPostDto
    {
        [Required]
        public string? Name { get; set; }
        public List<GroupPostDto>? Groups { get; set; }
    }
}
