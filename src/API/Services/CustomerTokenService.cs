using API.Models;
using API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace API.Services
{
    /// <summary>
    /// Provides token generation services for <see cref="Customer"/> entities.
    /// </summary>
    public class CustomerTokenService : ITokenService<Customer>
    {
        private readonly SymmetricSecurityKey _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerTokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration containing the JWT settings.</param>
        public CustomerTokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:CustomerSecret"]));
        }

        /// <summary>
        /// Generates a JWT token for the specified <see cref="Customer"/>.
        /// </summary>
        /// <param name="customer">The <see cref="Customer"/> for which the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public string GenerateToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Name, customer.CustomerName),
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
