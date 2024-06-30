using API.Models.Enums;

namespace API.Models.DTOs.CustomerDto
{
    public class ReturnCustomerOrderAddressDto
    {
        public AddressType Type { get; set; }
        public AddressCode Code { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
    }
}
