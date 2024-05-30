using API.Models.Enums;

namespace API.Models
{
    public class CashPayment
    {
        public int CashPaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal PayAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int ReceiveBy { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Employee Employee { get; set; }
    }
}
