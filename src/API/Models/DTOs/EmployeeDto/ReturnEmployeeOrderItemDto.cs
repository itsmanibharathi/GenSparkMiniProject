namespace API.Models.DTOs.EmployeeDto
{
    public class ReturnEmployeeOrderItemDto
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? RestaurantName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}