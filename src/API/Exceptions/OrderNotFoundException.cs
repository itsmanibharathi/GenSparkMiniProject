using System;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an order is not found.
    /// </summary>
    public class OrderNotFoundException : Exception
    {
        private readonly string message;

        /// <summary>
        /// Throw when order not found.
        /// </summary>
        /// <param name="orderId">The ID of the order that was not found.</param>
        public OrderNotFoundException(int orderId)
        {
            message = $"Order with ID {orderId} not found";
        }

        public override string Message => message;
    }
}
