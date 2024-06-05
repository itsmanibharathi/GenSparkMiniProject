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
        /// Initializes a new instance of the <see cref="InvalidOrderException"/> class with the specified order ID and status.
        /// </summary>
        /// <param name="orderId">The ID of the order that is invalid.</param>
        /// <param name="status">The status of the order that is invalid.</param>
        public InvalidOrderException(int orderId, string status)
        {
            message = $"Order {orderId} is in {status} status";
        }

        public override string Message => message;
    }
}
