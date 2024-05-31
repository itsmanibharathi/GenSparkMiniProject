using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.CustomerDto
{
    /// <summary>
    /// Parent Dto For CustomerOrderItemDto
    /// <para>To Collect the Order</para>
    /// </summary>
    public class CustomerOrderDto
    {
        /// <summary>
        /// custoemr id is mapp at the controller level from the JWT token
        /// </summary>
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Shipping address id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Shipping address id must be greater than 0")]

        public int ShippingAddressId { get; set; }
        [Required(ErrorMessage = "Order Product id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "To Create Order you have minimum of one Order Items")]
        public IEnumerable<CustomerOrderItemDto> OrderItemIds { get; set; }
    }
}
