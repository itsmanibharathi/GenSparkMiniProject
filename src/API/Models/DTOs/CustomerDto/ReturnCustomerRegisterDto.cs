namespace API.Models.DTOs.CustomerDto
{
    public class ReturnCustomerRegisterDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }

    }
}
