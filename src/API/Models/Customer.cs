using API.Models;

namespace API.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<CustomerAddress>? Addresses { get; set; }
        public CustomerAuth? CustomerAuth { get; set; }
    }
}