using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.CustomerDto
{
    public class CustomerOrderDto
    {
        public int CustomerId { get; set; }
        public int ShippingAddressId { get; set; }
        public IEnumerable<CustomerOrderItemDto> OrderItemIds { get; set; }
    }
}
