using API.Models.Enums;

namespace API.Models
{
    public class OnlinePayment
    {
        public int OnlinePaymentId { get; set; }
        public int CustomerId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentRef { get; set; } = string.Empty;
        public DateTime PaymentDate { get; internal set; }
        public decimal PaymentAmount { get; internal set; }
        public ICollection<Order>? Orders { get; set; }
        public Customer? Customer { get; set; }
    }
}
