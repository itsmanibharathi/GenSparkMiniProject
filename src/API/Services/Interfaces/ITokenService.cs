using API.Models;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Interface for token generation services.
    /// </summary>
    /// <typeparam name="T">The type of the item for which the token is generated. Must be a class.</typeparam>
    public interface ITokenService<T> where T : class
    {
        /// <summary>
        /// Generates a token for the specified item.
        /// </summary>
        /// <param name="item">The item for which the token is generated.</param>
        /// <returns>The generated token as a string.</returns>
        string GenerateToken(T item);
    }
}
