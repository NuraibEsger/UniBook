namespace UniBook.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public List<Semester>? Semesters { get; set; }
    }
}
