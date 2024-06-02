using System.Runtime.Serialization;

namespace API.Exceptions
{
    [Serializable]
    internal class EntityNotFoundException<Entity> : Exception
    {
        string message;
        public EntityNotFoundException()
        {
            message = $"Entity of type {typeof(Entity)} not found";
        }
        public EntityNotFoundException(int id)
        {
            message = $"Entity of type {typeof(Entity)} with id {id} not found";
        }
        public EntityNotFoundException(string email)
        {
            message = $"Entity of type {typeof(Entity)} with email {email} not found";
        }
        public override string Message => message;
    }
}