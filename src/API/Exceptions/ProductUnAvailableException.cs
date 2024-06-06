using System;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a product is not available.
    /// </summary>
    public class ProductUnAvailableException : Exception
    {
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUnAvailableException"/> class with a default message.
        /// </summary>
        public ProductUnAvailableException()
        {
            message = "Product is not available";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUnAvailableException"/> class with the specified product name.
        /// </summary>
        /// <param name="productName">The name of the product that is unavailable.</param>
        public ProductUnAvailableException(string productName)
        {
            message = $"{productName} is Un Available";
        }
        public override string Message => message;
    }
}
