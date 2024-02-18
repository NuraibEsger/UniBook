namespace UniBook.Entities
{
    public class StudentDetails
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
