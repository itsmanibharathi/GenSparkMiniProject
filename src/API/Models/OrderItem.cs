namespace API.Models
{
    public class OrderItem : IEquatable<OrderItem>
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
        public bool Equals(OrderItem? other)
        {
            if (other == null)
            {
                return false;
            }
            return OrderId == other.OrderId &&
                   ProductId == other.ProductId &&
                   Quantity == other.Quantity;
                   
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals(obj as Order);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OrderId, ProductId, Quantity);
        }
    }
}
