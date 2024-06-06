using System;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when user credentials are invalid.
    /// </summary>
    public class InvalidUserCredentialException : Exception
    {
        private readonly string message;

        /// <summary>
        /// Invalid User Credential Exception Constructor
        /// </summary>
        public InvalidUserCredentialException()
        {
            message = "Invalid User Credential";
        }
        public override string Message => message;
    }
}
