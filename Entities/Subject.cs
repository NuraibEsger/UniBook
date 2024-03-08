namespace UniBook.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Exam>? Exams { get; set; }
        public List<AppUser>? Users { get; set; }
    }
}
