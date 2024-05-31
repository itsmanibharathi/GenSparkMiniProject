using API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.CustomerDto
{
    /// <summary>
    /// To create a new shipping address for the customer 
    /// </summary>
    public class CustomerAddressDto
    {
        /// <summary>
        /// custoemr id is mapp at the controller level from the JWT token
        /// </summary>
        public int CustomerId { get; set; }
        
        [Required(ErrorMessage = "Address type is required")]
        [EnumDataType(typeof(AddressType), ErrorMessage = "Invalid address type")]
        public AddressType Type { get; set; }

        [Required(ErrorMessage = "Address code is required")]
        [EnumDataType(typeof(AddressCode), ErrorMessage = "Invalid address code")]
        public AddressCode Code { get; set; }
        
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

    }
}
