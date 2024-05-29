using API.Models;

namespace API.Models
{
    public class CustomerAuth
    {
        public int CustomerId { get; set; }
        public string Password { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public Customer? Customer { get; set; }
    }
}
