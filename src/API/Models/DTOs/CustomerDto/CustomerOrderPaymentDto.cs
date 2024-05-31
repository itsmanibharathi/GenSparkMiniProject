using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.CustomerDto
{
    /// <summary>
    /// Create a new orders Payment for the customer
    /// </summary>
    public class CustomerOrderPaymentDto
    {
        /// <summary>
        /// custoemr id is mapp at the controller level from the JWT token
        /// </summary>
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "order ids is required")]
        [MinLength(1, ErrorMessage = "Order ids must be at least 1")]
        public ICollection<int> Orders { get; set; }

        [Required(ErrorMessage = "Payment method is required")]
        [EnumDataType(typeof(PaymentMethod), ErrorMessage = "Invalid payment method")]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
