namespace UniBook.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Course>? Courses { get; set; }
        public List<UserGroup>? UserGroups { get; set; }
    }
}
