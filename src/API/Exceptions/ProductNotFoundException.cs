namespace API.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        string message;
        public ProductNotFoundException()
        {
            message = "Product Not Found";
        }
        public ProductNotFoundException(string Name) 
        {
            message = $"Product with Name {Name} Not Found";
        }
        public ProductNotFoundException(int id)
        {
            message = $"Product with Id {id} Not Found";
        }
        public override string Message => message;
    }
}
