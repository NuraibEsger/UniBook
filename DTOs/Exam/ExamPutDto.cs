namespace UniBook.DTOs.Exam
{
    public class ExamPutDto
    {
        public int ExamId { get; set; }
        public int? SubjectId { get; set; }
        public int GroupId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
