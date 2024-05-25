namespace API.Models
{
    public class EmployeeAuth
    {
        public int EmployeeId { get; set; }
        public string Password { get; set; }
        public Employee? Employee { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
    }
}
