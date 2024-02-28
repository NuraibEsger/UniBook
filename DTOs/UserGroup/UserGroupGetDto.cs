using Microsoft.AspNetCore.Identity;

namespace UniBook.DTOs.UserGroup
{
    public class UserGroupGetDto
    {
        public int Id { get; set; }
        public string? User { get; set; }
        public string? GroupName { get; set; }
        public int? GroupId { get; set; }
    }
}
