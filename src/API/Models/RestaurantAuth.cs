namespace API.Models
{
    public class RestaurantAuth
    {
        public int RestaurantId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
