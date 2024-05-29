namespace API.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        string message ;
        public CustomerNotFoundException()
        {
            message = "Customer not found";
        }
        public CustomerNotFoundException(int id)
        {
            message = $"Customer with id {id} not found";
        }
        public CustomerNotFoundException(string email)
        {
            message = $"Customer with email {email} not found";
        }
        public override string Message => message;
    }
}
