using API.Models.Enums;

namespace API.Models.DTOs
{
    public class ReturnCreateCustomerOrderDto
    {
        public int OrderId { get; set; }
        public int RestaurantId { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public decimal TaxRat { get; set; }
        public decimal DiscountRat { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
