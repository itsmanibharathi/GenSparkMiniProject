namespace API.Exceptions
{
    public class EntityAlreadyExistsException <Enetity> : Exception
    {
        string message;
        public object Data;
        public EntityAlreadyExistsException(object data)
        {
            message = $"Entity of type {typeof(Enetity)} already exists";
            Data = data;
        }
        public override string Message => message;
    }
}
