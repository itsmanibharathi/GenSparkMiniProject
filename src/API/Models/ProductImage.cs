namespace API.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public string ImageDescription { get; set; }
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public Product Product { get; set; }
    }
}