namespace API.Models.DTOs.CustomerDto
{
    public class ReturnCustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
