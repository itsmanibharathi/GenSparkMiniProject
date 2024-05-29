using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs
{
    public class CreateCustomerOrderDto
    {
        public int CustomerId { get; set; }
        public int ShippingAddressId { get; set; }
        public IEnumerable<OrderItemDto> OrderItemIds { get; set; }
    }
}
