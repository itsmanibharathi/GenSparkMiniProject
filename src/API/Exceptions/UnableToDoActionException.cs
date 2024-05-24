namespace API.Exceptions
{
    public class UnableToDoActionException : Exception
    {
        string message;
        public UnableToDoActionException()
        {
            message = "Unable to Done the Action";
        }
        public UnableToDoActionException(string message)
        {
            this.message = message;
        }
        public UnableToDoActionException(string message, Exception ex) : base(message, ex)
        {
            this.message = message;
        }
        public override string Message => message;
    }
}
