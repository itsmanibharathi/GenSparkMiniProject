using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.EmployeeDto
{
    /// <summary>
    /// Create new employee Account
    /// </summary>
    public class EmployeeRegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmployeeEmail { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string EmployeePhone { get; set; }
        public string EmployeeAddress { get; set; }

        [Required(ErrorMessage = "Address code is required")]
        [EnumDataType(typeof(AddressCode), ErrorMessage = "Invalid address code")]
        public AddressCode AddressCode { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(20, ErrorMessage = "Password must be at most 20 characters")]
        public string Password { get; set; }
    }
}
