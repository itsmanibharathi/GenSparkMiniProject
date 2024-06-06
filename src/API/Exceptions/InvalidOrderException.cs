using System;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an order is invalid.
    /// </summary>
    public class InvalidOrderException : Exception
    {
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOrderException"/> class with a default message.
        /// </summary>
        public InvalidOrderException()
        {
            message = "Something went wrong!";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOrderException"/> class with the specified message.
        /// </summary>
        /// <param name="message">The message that describes the exception.</param> 

        public InvalidOrderException(string message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}
