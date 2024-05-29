namespace API.Exceptions
{
    public class RestaurantNotFoundException : Exception
    {
        string message;
        public RestaurantNotFoundException()
        {
            message = "No Restaurant in this Id";
        }
        public RestaurantNotFoundException(int id)
        {
            message = $"No Restaurant in this Id {id}";
        }
        public override string Message => message;
    }
}
