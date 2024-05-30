namespace API.Models.DTOs
{
    public class ReturnRestaurantLoginDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
