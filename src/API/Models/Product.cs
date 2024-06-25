using API.Models.Enums;

namespace API.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int RestaurantId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductAvailable { get; set; } 
        public ProductCategory ProductCategories { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public Restaurant? Restaurant { get; set; }
        public IEnumerable<OrderItem>? OrderItems { get; set; }
    }
}
