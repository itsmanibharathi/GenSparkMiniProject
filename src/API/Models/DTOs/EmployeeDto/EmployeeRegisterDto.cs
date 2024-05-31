using API.Models.Enums;

namespace API.Models.DTOs.EmployeeDto
{
    public class EmployeeRegisterDto
    {
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeAddress { get; set; }
        public AddressCode AddressCode { get; set; }
        public string Password { get; set; }
    }
}
