using API.Models.Enums;

namespace API.Models.DTOs.CustomerDto
{
    public class ReturnCustomerAddressDto
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
        public DateTime UpdateAt { get; set; }
    }
}
