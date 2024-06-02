using API.Models.Enums;

namespace API.Models
{
    public class CashPayment
    {
        public int CashPaymentId { get; set; }
        public DateTime PaymentDate { get;  set; }
        public decimal PaymentAmount { get;  set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int? ReceiveBy { get; set; }
        public Employee? Employee { get; set; }
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
