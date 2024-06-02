namespace API.Exceptions
{
    public class InvalidUserCredentialException : Exception
    {
        string message;

        public InvalidUserCredentialException()
        {
            message = "Invalid User Credential";
        }

        public InvalidUserCredentialException(Exception ex) : base(null,ex)
        {
            message = "Invalid User Credential";
        }
        public override string Message => message;
    }
}
