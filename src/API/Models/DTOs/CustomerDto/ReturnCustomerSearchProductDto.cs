using API.Models.Enums;

namespace API.Models.DTOs.CustomerDto
{
    public class ReturnCustomerSearchProductDto
    {
        public int ProductId { get; set; }
        public int RestaurantId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductAvailable { get; set; }
        public ProductCategory ProductCategories { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantBranch { get; set; }
    }
}
