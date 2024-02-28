namespace UniBook.Entities
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int GroupId { get; set; }
        public AppUser? User { get; set; }
        public Group? Group { get; set; }
    }
}
