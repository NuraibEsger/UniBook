using System.ComponentModel.DataAnnotations;
using UniBook.DTOs.Student;

namespace UniBook.DTOs.Group
{
    public class GroupPutDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int DepartmentId { get; set; }
    }
}
