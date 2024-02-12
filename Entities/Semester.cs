namespace UniBook.Entities
{
    public class Semester
    {
        public int Id { get; set; }
        public string? SemesterName { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
