namespace API.Exceptions
{
    public class ProductUnAvailableException : Exception
    {
        string mesaage;
        public ProductUnAvailableException()
        {
            mesaage = "Product is not available";
        }
        public ProductUnAvailableException(string productName)
        {
            mesaage = $"{productName} is Un Available.";
        }
        public override string Message => mesaage;
    }
}
