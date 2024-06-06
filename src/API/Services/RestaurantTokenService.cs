using API.Models;
using API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;

namespace API.Services
{
    /// <summary>
    /// Provides token generation services for <see cref="Restaurant"/> entities.
    /// </summary>
    public class RestaurantTokenService : ITokenService<Restaurant>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantTokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration containing the JWT settings.</param>
        public RestaurantTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JWT token for the specified <see cref="Restaurant"/>.
        /// </summary>
        /// <param name="item">The <see cref="Restaurant"/> for which the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public string GenerateToken(Restaurant item)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:RestaurantSecret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", item.RestaurantId.ToString()),
                    new Claim(ClaimTypes.Name, item.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
