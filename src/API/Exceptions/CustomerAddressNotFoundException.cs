namespace API.Exceptions
{
    public class CustomerAddressNotFoundException : Exception
    {
        string message;
        public CustomerAddressNotFoundException()
        {
            this.message = "Customer Address Not Found";
        }
        public CustomerAddressNotFoundException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }
}
