namespace API.Services.Interfaces
{
    /// <summary>
    /// Interface for password hashing and verification services.
    /// </summary>
    public interface IPasswordHashService
    {
        /// <summary>
        /// Hashes the specified password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password.</returns>
        string Hash(string password);

        /// <summary>
        /// Verifies the specified password against the given password hash.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="passwordHash">The hashed password to compare against.</param>
        /// <returns><c>true</c> if the password matches the hash; otherwise, <c>false</c>.</returns>
        bool Verify(string password, string passwordHash);
    }
}
