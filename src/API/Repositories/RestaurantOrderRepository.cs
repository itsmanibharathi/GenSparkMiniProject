using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RestaurantOrderRepository : OrderRepository, IRestaurantOrderRepository
    {
        public RestaurantOrderRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAllByRestaurantId(int restaurantId)
        {
            try
            {
                var res = await _context.Orders.Where(o => o.RestaurantId == restaurantId).ToListAsync();
                return res.Count() > 0 ? res : throw new EntityNotFoundException<Order>();
            }
            catch(EntityNotFoundException<Order>)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new UnableToDoActionException($"Unable to get all orders by {restaurantId}", ex);
            }

        }

        public async Task<IEnumerable<Order>> GetToDayByRestaurantId(int restaurantId)
        {
            try
            {
                var res = await _context.Orders.Where(o => o.RestaurantId == restaurantId && o.OrderDate.Date == DateTime.Now.Date).ToListAsync();
                return res.Count() > 0 ? res : throw new EntityNotFoundException<Order>();
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException($"Unable to get today's orders by {restaurantId}", ex);
            }
        }
    }
}
