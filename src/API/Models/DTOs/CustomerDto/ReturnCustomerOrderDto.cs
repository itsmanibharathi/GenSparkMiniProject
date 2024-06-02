using API.Models.Enums;

namespace API.Models.DTOs.CustomerDto
{
    public class ReturnCustomerOrderDto
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int RestaurantId { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; } 
        public DateTime DeliveryDate { get; set; }
    }
}
