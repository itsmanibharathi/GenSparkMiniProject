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
    /// Provides token generation services for <see cref="Employee"/> entities.
    /// </summary>
    public class EmployeeTokenService : ITokenService<Employee>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeTokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration containing the JWT settings.</param>
        public EmployeeTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JWT token for the specified <see cref="Employee"/>.
        /// </summary>
        /// <param name="item">The <see cref="Employee"/> for which the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public string GenerateToken(Employee item)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:EmployeeSecret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", item.EmployeeId.ToString()),
                    new Claim(ClaimTypes.Name, item.EmployeeName)
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
