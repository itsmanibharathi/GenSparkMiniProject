using API.Models.Enums;

namespace API.Models.DTOs
{
    public class ReturnOrderPaymentDto
    {
        public int? CashPaymentId { get; set; }
        public int? OnlinePaymentId { get; set; }
        public ICollection<ReturnCreateCustomerOrderDto>? Orders { get; set; }
        public decimal PaymentAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
