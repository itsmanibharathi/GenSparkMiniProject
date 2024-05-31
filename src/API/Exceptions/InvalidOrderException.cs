namespace API.Exceptions
{
    public class InvalidOrderException : Exception
    {
        string message;
        public InvalidOrderException()
        {
            this.message = "Something went wrong!";
        }
        public InvalidOrderException(int orderid,string status)
        {
            this.message = $"Order {orderid} is in {status} status";
        }
        public override string Message => message;


    }
}
