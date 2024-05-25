using API.Models.Enums;

namespace API.Models.DTOs
{
    public class ReturnEmployeeRegisterDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
    }
}
