using API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace API.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public const char Delimiter = ';';

        public string Hash(string password)
        {
            HMACSHA512 hMACSHA = new HMACSHA512();

            var PasswordHashKey = hMACSHA.Key;
            var PasswordHash = hMACSHA.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return string.Join(Delimiter, Convert.ToBase64String(PasswordHashKey), Convert.ToBase64String(PasswordHash));
        }

        public bool Verify(string password, string passwordHash)
        {
            var passwordHashParts = passwordHash.Split(Delimiter);
            var PasswordHashKey = Convert.FromBase64String(passwordHashParts[0]);
            var PasswordHash = Convert.FromBase64String(passwordHashParts[1]);

            HMACSHA512 hMACSHA = new HMACSHA512(PasswordHashKey);
            var computedPasswordHash = hMACSHA.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return PasswordHash.SequenceEqual(computedPasswordHash);
        }
    }
}
