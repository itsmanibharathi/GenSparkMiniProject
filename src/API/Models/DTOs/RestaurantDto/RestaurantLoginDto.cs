using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.RestaurantDto
{
    public class RestaurantLoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(20, ErrorMessage = "Password must be at most 20 characters")]
        public string Password { get; set; }
    }
}
