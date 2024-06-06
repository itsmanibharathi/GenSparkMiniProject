using System;

namespace API.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a product is not found.
    /// </summary>
    public class ProductNotFoundException : Exception
    {
        private readonly string message;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class with a default message.
        /// </summary>
        public ProductNotFoundException()
        {
            message = "Product Not Found";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class with the specified product name.
        /// </summary>
        /// <param name="name">The name of the product that was not found.</param>
        public ProductNotFoundException(string name)
        {
            message = $"Product with Name '{name}' Not Found";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductNotFoundException"/> class with the specified product ID.
        /// </summary>
        /// <param name="id">The ID of the product that was not found.</param>
        public ProductNotFoundException(int id)
        {
            message = $"Product with ID {id} Not Found";
        }

        public override string Message => message;
    }
}
