using System;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a Enitity is already up to date.
    /// </summary>
    public class AlreadyUptoDateException : Exception
    {
        private readonly string message;

        /// <summary>
        /// Throw by Id.
        /// </summary>
        /// <param name="id">The ID of the Entity that is already up to date.</param>
        public AlreadyUptoDateException(int id)
        {
            message = $"Entity with id {id} is already up to date";
        }

        /// <summary>
        /// Throw with message.
        /// </summary>
        /// <param name="message">The message that describes the exception.</param>
        public AlreadyUptoDateException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }
}
