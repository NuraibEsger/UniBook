namespace UniBook.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Group>? Groups { get; set; }
    }
}
