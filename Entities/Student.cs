﻿namespace UniBook.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int GroupId { get; set; }
        public Group? Group { get; set; }
        public List<StudentTeacher>? StudentTeachers { get; set; }
    }
}
