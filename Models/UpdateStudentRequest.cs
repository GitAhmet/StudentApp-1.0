namespace StudentApp.Models
{
    public class UpdateStudentRequest
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
        public string? TlfNo { get; set; }
        public string? School { get; set; }
    }
}
