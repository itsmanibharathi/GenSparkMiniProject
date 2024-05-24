namespace API.Exceptions
{
    public class NoEmployeeInThisIdException : Exception
    {
        string message;
        public NoEmployeeInThisIdException()
        {
            message = "No Employee in this Id";
        }
        public NoEmployeeInThisIdException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }
}
