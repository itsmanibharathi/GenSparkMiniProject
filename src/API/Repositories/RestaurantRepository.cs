using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace API.Repositories
{
    public class RestaurantRepository : IRepository<int, Restaurant>
    {
        private readonly DBGenSparkMinirojectContext _context;

        [ExcludeFromCodeCoverage]
        public RestaurantRepository(DBGenSparkMinirojectContext context) 
        {
            _context = context;
        }
        public async Task<Restaurant> Add(Restaurant entity)
        {
            try
            {
                if (!await Duplicate(entity))
                {
                    await _context.Restaurants.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
                throw new DataDuplicateException("Email or Fssai License Number already exists");
            }
            catch (DataDuplicateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable To Add the Restaurant", ex);
            }
        }

        [ExcludeFromCodeCoverage]
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Restaurant> Get(int id)
        {
            try
            {
                return await _context.Restaurants.SingleOrDefaultAsync(r => r.RestaurantId == id)?? throw new RestaurantNotFoundException(id);
            }
            catch (RestaurantNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Restaurant", ex);
            }
        }

        public async Task<IEnumerable<Restaurant>> Get()
        {
            try
            {
                var res = await _context.Restaurants.ToListAsync();
                return res.Count > 0 ? res : throw new EmptyDatabaseException("Restaurant DB Empty");
            }
            catch (EmptyDatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get All the Restaurants", ex);
            }
        }

        public async Task<Restaurant> Update(Restaurant entity)
        {
            try
            {
                _context.Restaurants.Update(entity);
                var res = await _context.SaveChangesAsync();
                return res > 0 ? entity : throw new UnableToDoActionException("Unable to update the Restaurant");
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to update the Restaurant", ex);
            }
        }
        [ExcludeFromCodeCoverage]
        private async Task<bool> Duplicate(Restaurant entity)
        {
            return await _context.Restaurants.AnyAsync(x => x.Email == entity.Email || x.FssaiLicenseNumber == entity.FssaiLicenseNumber);
        }
    }
}
