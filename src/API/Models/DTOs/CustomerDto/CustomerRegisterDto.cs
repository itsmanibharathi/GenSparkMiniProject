using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.CustomerDto
{
    /// <summary>
    /// Create new customer Account
    /// </summary>
    public class CustomerRegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string CustomerEmail { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string? CustomerPhone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(20, ErrorMessage = "Password must be at most 20 characters")]
        public string CustomerPassword { get; set; }
    }
}
