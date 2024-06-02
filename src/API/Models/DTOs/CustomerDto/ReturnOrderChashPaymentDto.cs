using API.Models.Enums;

namespace API.Models.DTOs.CustomerDto
{
    public class ReturnOrderCashPaymentDto
    {
        public int CashPaymentId { get; set; }
        public ReturnCustomerOrderDto? Order { get; set; }
        public decimal PaymentAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
