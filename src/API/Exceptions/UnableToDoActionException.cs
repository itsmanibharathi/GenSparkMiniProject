using System;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an action cannot be performed.
    /// </summary>
    public class UnableToDoActionException : Exception
    {
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnableToDoActionException"/> class with a default message.
        /// </summary>
        public UnableToDoActionException()
        {
            message = "Unable to Done the Action";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnableToDoActionException"/> class with a specified message.
        /// </summary>
        /// <param name="message">The message that describes the exception.</param>
        public UnableToDoActionException(string message)
        {
            this.message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnableToDoActionException"/> class with a specified message and inner exception.
        /// </summary>
        /// <param name="message">The message that describes the exception.</param>
        /// <param name="ex">The inner exception that caused the current exception.</param>
        public UnableToDoActionException(string message, Exception ex) : base(message, ex)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}
