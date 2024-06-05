using System;
using System.Runtime.Serialization;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an entity is not found.
    /// </summary>
    /// <typeparam name="Entity">The type of the entity.</typeparam>
    [Serializable]
    public class EntityNotFoundException<Entity> : Exception
    {
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException{Entity}"/> class with a default message.
        /// </summary>
        public EntityNotFoundException()
        {
            message = $"Entity of type {typeof(Entity)} not found";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException{Entity}"/> class with a specified entity ID.
        /// </summary>
        /// <param name="id">The ID of the entity that was not found.</param>
        public EntityNotFoundException(int id)
        {
            message = $"Entity of type {typeof(Entity)} with id {id} not found";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException{Entity}"/> class with a specified entity email.
        /// </summary>
        /// <param name="email">The email of the entity that was not found.</param>
        public EntityNotFoundException(string email)
        {
            message = $"Entity of type {typeof(Entity)} with email {email} not found";
        }

        public override string Message => message;
    }
}
