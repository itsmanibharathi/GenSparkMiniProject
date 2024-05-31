using API.Models.Enums;

namespace API.Models.DTOs.CustomerDto
{
    public class CustomerSearchProductDto
    {
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public bool ProductAvailable { get; set; } = true;
        public ICollection<ProductCategory>? ProductCategories { get; set; }
        public string? RestaurantName { get; set; }
        public string? RestaurantBranch { get; set; }

    }

}
