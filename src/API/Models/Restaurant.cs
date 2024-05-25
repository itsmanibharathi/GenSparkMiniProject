using API.Models.Enums;

namespace API.Models
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Branch { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int FssaiLicenseNumber { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public bool IsActive { get; set; }
        public AddressCode AddressCode { get; set; }
        public RestaurantAuth? RestaurantAuth { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
