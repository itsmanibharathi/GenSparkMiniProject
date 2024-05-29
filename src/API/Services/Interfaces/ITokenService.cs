using API.Models;

namespace API.Services.Interfaces
{
    public interface ITokenService <T> where T : class
    {
        public string GenerateToken(T item);
    }
}
