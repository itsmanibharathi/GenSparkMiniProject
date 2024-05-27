namespace API.Models.DTOs
{
    public class ReturnCreateCustomerOrderDto
    {
        public int OrderId { get; set; }
        public decimal TotalOrderPrice { get; set; }
        public decimal TotalShipping { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
