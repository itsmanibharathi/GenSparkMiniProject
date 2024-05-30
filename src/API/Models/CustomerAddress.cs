using API.Models;
using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CustomerAddress
    {
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Customer Id is required for Create address")]
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Address Type is required")]
        public AddressType Type { get; set; }
        [Required(ErrorMessage = "Address Code is required")]
        public AddressCode Code { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<Order>? Orders { get; set; }   

    }
}
