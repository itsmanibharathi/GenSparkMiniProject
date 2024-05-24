namespace API.Exceptions
{
    public class DataDuplicateException : Exception
    {
        string message;
        public DataDuplicateException()
        {
            message = "already exists";
        }

        public DataDuplicateException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }
}
