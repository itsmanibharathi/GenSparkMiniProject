namespace API.Exceptions
{
    public class EmptyDatabaseException : Exception
    {
        string message;
        public EmptyDatabaseException()
        {
            message = "Database is Empty";
        }
        public EmptyDatabaseException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }
}
