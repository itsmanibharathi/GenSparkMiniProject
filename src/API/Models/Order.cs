using API.Models.Enums;

namespace API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerAddressId { get; set; }
        public int EmployeeId { get; set; }
        public int RestaurantId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        
        public decimal ShippingPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal _totalOrderPrice;
        public decimal TotalTax;
        public decimal TotalAmount { get; set; }
        public decimal TotalOrderPrice {
            get
            {
                return _totalOrderPrice;
            }
            set 
            {
                _totalOrderPrice = value;
                TotalTax = _totalOrderPrice * 0.1m;  // 10% tax
                DiscountPrice = _totalOrderPrice * 0.05m; // 5% discount
                TotalAmount = _totalOrderPrice + TotalTax + ShippingPrice - DiscountPrice;
            } 
        }
        public Customer Customer { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public Employee Employee { get; set; }
        public Restaurant Restaurant { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
    }
}
