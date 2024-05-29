namespace API.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        string message;
        public EmployeeNotFoundException()
        {
            message = "No Employee in this Id";
        }
        public EmployeeNotFoundException(int id)
        {
            message = $"No Employee in this Id {id}";
        }
        public override string Message => message;
    }
}
