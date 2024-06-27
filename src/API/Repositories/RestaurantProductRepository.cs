using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RestaurantProductRepository : ProductRepository, IRestaurantProductRepository
    {
        public RestaurantProductRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        public async Task<Product> GetAsync(int productId)
        {
            try
            {
                var res = await _context.Products
                .Include(x => x.Restaurant)
                .FirstOrDefaultAsync(x => x.ProductId == productId);
                return res ?? throw new EntityNotFoundException<Product>(productId);
            }
            catch (EntityNotFoundException<Product>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Product", ex);
            }
        }


        public async Task<IEnumerable<Product>> GetByRestaurantIdAsync(int restaurantId)
        {
            try
            {
                var res = await _context.Products
                .Include(x => x.Restaurant)
                .Where(x => x.RestaurantId == restaurantId).ToListAsync();
                return res.Count > 0 ? res : throw new EntityNotFoundException<Product>(restaurantId);
            }
            catch (EntityNotFoundException<Product>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Products by Restaurant Id", ex);
            }
        }
    }
}
