using API.Models.Enums;

namespace API.Models
{
    public class OnlinePayment
    {
        public int OnlinePaymentId { get; set; }
        public int OrderId { get; set; }
        public decimal PayAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentRef { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
