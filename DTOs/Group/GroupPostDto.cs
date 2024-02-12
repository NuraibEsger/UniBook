using System.ComponentModel.DataAnnotations;
using UniBook.DTOs.Student;

namespace UniBook.DTOs.Group
{
    public class GroupPostDto
    {
        [Required]
        public string? Name { get; set; }
        public List<StudentPostDto>? Students { get; set; }

    }
}
