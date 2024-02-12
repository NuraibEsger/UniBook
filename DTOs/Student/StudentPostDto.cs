using System.ComponentModel.DataAnnotations;

namespace UniBook.DTOs.Student
{
    public class StudentPostDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
    }
}
