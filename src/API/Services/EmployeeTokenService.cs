using API.Models;
using API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class EmployeeTokenService : ITokenService<Employee>
    {
        private readonly IConfiguration _configuration;

        public EmployeeTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
