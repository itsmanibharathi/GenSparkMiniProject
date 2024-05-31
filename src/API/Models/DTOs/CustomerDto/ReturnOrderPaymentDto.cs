using API.Models.Enums;

namespace API.Models.DTOs.CustomerDto
{
    public class ReturnOrderPaymentDto
    {
        public int? CashPaymentId { get; set; }
        public int? OnlinePaymentId { get; set; }
        public ICollection<ReturnCustomerOrderDto>? Orders { get; set; }
        public decimal PaymentAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
