using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RestaurantAuthRepository : IRestaurantAuthRepository
    {
        private readonly DBGenSparkMinirojectContext _context;
        public RestaurantAuthRepository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }
        public async Task<Restaurant> Get(string email)
        {
            try
            {
                return (await _context
                    .Restaurants
                    .Include(r => r.RestaurantAuth)
                    .FirstOrDefaultAsync(r => r.Email == email)
                    ?? throw new InvalidUserCredentialException());
            }
            catch (InvalidUserCredentialException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Login", ex);
            }
        }
    }
}
