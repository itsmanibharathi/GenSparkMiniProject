using System;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an entity already exists.
    /// </summary>
    /// <typeparam name="Entity">The type of the entity.</typeparam>
    public class EntityAlreadyExistsException<Entity> : Exception
    {
        private readonly string message;

        public new object Data { get; }

        /// <summary>
        /// EntityAlreadyExistsException constructor
        /// </summary>
        /// <param name="data">The data associated with the entity that already exists.</param>
        public EntityAlreadyExistsException(object data)
        {
            message = $"Entity of type {typeof(Entity)} already exists";
            Data = data;
        }

        public override string Message => message;
    }
}
