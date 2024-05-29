using API.Models.Enums;

namespace API.Models.DTOs
{
    public class RestaurantRegisterDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public AddressCode AddressCode { get; set; }
        public int FssaiLicenseNumber { get; set; }
        public string Password { get; set; }

    }
}
