namespace UniBook.Entities
{
    public class Exam
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int GroupId { get; set; }
        public Group? Group { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
