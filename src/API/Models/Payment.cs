using API.Models.Enums;

namespace API.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int? TransactionId { get; set; }
        public Order? Order { get; set; }
    }
}
