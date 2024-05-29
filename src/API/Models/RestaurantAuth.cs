namespace API.Models
{
    public class RestaurantAuth
    {
        public int RestaurantId { get; set; }
        public string Password { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
