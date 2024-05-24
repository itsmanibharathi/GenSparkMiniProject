using API.Models;
using API.Models.Enums;

namespace API.Models
{
    public class CustomerAddress
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public AddressType Type { get; set; }
        public AddressCode Code { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

        public Customer? Customer { get; set; }

    }
}
