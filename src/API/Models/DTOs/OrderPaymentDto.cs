using API.Models.Enums;

namespace API.Models.DTOs
{
    public class OrderPaymentDto
    {
        public int CustomerId { get; set; }
        public ICollection<int> Orders { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
