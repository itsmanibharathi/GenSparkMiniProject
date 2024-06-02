namespace API.Exceptions
{
    public class OrderNotFoundException : Exception
    {
        string message;
        public OrderNotFoundException(int orderId)
        {
            message = "Order not found";
        }
        public override string Message => message;

    }
}
