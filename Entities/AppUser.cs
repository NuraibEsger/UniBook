﻿using Microsoft.AspNetCore.Identity;

namespace UniBook.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public List<UserGroup>? UserGroups { get; set; }
        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
