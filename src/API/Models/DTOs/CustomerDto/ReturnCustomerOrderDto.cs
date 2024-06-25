using API.Models.Enums;

namespace API.Models.DTOs.CustomerDto
{
    public class ReturnCustomerOrderDto
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxRat { get; set; }
        public decimal DiscountRat { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public IEnumerable<ReturnCustomerOrderItemDto>? OrderItems { get; set; }
        public DateTime OrderDate { get; set; } 
        public DateTime DeliveryDate { get; set; }
    }
}
