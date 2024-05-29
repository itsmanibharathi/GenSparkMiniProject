using API.Models.Enums;

namespace API.Models.DTOs
{
    public class ReturnRestaurantProductDto
    {
        public int ProductId { get; set; }
        public int RestaurantId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductAvailable { get; set; }
        public ProductCategory ProductCategories { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
