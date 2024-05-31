using API.Models.Enums;

namespace API.Models
{
    public class CashPayment
    {
        public int CashPaymentId { get; set; }
        public DateTime PaymentDate { get; internal set; }
        public decimal PaymentAmount { get; internal set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int? ReceiveBy { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
