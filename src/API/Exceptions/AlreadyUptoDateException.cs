namespace API.Exceptions
{
    public class AlreadyUptoDateException : Exception
    {
        string message;
        public AlreadyUptoDateException(int productId)
        {
            message = $"Product with id {productId} is already upto date";
        }
        public AlreadyUptoDateException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }
}
