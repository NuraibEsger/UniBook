using System.ComponentModel.DataAnnotations;

namespace UniBook.DTOs.Subject
{
    public class SubjectPutDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
