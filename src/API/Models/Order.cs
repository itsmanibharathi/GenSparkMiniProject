using API.Models.Enums;

namespace API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerAddressId { get; set; }
        public int RestaurantId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public DateTime DeliveryDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxRat { get; set; }
        public decimal DiscountRat { get; set; }
        public decimal _totalOrderPrice;
        public decimal TotalOrderPrice 
        {
            get
            {
                return _totalOrderPrice;
            }
            set 
            {
                _totalOrderPrice = value;
                TotalAmount = _totalOrderPrice + calPercentage(TaxRat) + ShippingPrice - calPercentage(DiscountRat) ;
            }
        }
        public int? OnlinePaymentId { get; set; }
        public OnlinePayment? OnlinePayment { get; set; }
        public int? CashPaymentId { get; set; }
        public CashPayment? CashPayment { get; set; }
        public Customer? Customer { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public Employee? Employee { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }        
        decimal calPercentage(decimal percentage)
        {
            if (percentage == 0)
            {
                return 0;
            }
            return (_totalOrderPrice * percentage);
        }
    }
}
