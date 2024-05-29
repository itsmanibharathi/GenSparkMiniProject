namespace API.Models.DTOs
{
    public class ReturnRestaurantLoginDto
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string Token { get; set; }
    }
}
