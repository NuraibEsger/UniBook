using System.ComponentModel.DataAnnotations;
using UniBook.Entities;

namespace UniBook.DTOs.Exam
{
    public class ExamGetDto
    {
        [Required]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string? GroupName { get; set; }
        public string? SubjectName { get; set; }
    }
}
