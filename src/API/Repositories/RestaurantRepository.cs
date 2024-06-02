using API.Context;
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

        public override Task<bool> IsDuplicate(Restaurant entity)
        {
            return _context.Restaurants.AnyAsync(x => x.Email == entity.Email && x.FssaiLicenseNumber== entity.FssaiLicenseNumber);
        }
    }
}
