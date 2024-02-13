namespace UniBook.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int GroupId { get; set; }
        public Group? Group { get; set; }
        public List<Semester>? Semesters { get; set; }
    }
}
