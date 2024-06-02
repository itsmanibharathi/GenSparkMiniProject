using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RestaurantRepository : Repository<int, Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        public async Task<Restaurant> GetByEmailId(string emailId)
        {
            try
            {
                return await _context.Restaurants.Include(x => x.RestaurantAuth).FirstOrDefaultAsync(x => x.Email == emailId)?? throw new EntityNotFoundException<Restaurant>(emailId);
            }
            catch (EntityNotFoundException<Restaurant>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Restaurant by Email Id", ex);
            }
        }

        public override Task<bool> IsDuplicate(Restaurant entity)
        {
            return _context.Restaurants.AnyAsync(x => x.Email == entity.Email && x.FssaiLicenseNumber== entity.FssaiLicenseNumber);
        }
    }
}
