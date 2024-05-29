namespace API.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        string message;
        public OrderNotFoundException()
        {
            message = "Order not found";
        }
        public override string Message => message;

    }
}
