using API.Models.Enums;

namespace API.Models.DTOs.EmployeeDto
{
    public class ReturnEmployeeOrderDto
    {
        public int OrderId { get; set; }
        public int RestaurantId { get; set; }
        public string? EmployeeName { get; set; }
        public string? CustomerName { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<ReturnEmployeeOrderItemDto>? OrderItems { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
