using API.Models;

namespace API.Models
{
    public class CustomerAuth
    {
        public int CustomerId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public Customer customer { get; set; }
    }
}
