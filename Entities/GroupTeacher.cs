﻿namespace UniBook.Entities
{
    public class GroupTeacher
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }
        public Group? Group { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
