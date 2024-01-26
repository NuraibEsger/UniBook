namespace UniBook.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subjects { get; set; }
        public List<StudentTeacher>? StudentTeachers { get; set; }
        public List<GroupTeacher>? GroupTeachers { get; set; }
    }
}
