namespace UniBook.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Student>? Students { get; set; }
        public List<GroupTeacher>? GroupTeachers { get; set; }
    }
}
