using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using API.Models.Enums;
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
                var res = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Include(o => o.Restaurant)
                .Include(o => o.Employee)
                .Include(o => o.Customer)
                .Where(o => o.RestaurantId == restaurantId)
                .Where(o => o.OrderStatus != OrderStatus.Create && o.OrderStatus != OrderStatus.Cancelled)
                .ToListAsync();
                return res.Count() > 0 ? res : throw new EntityNotFoundException<Order>();
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException($"Unable to get all orders by {restaurantId}", ex);
            }

        }

        public async Task<IEnumerable<Order>> GetToDayByRestaurantId(int restaurantId)
        {
            try
            {
                var res = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Restaurant)
                .Include(e => e.Employee)
                .Include(c => c.Customer)
                .Where(o => o.RestaurantId == restaurantId && o.OrderDate.Date == DateTime.Now.Date)
                .Where(o => o.OrderStatus != OrderStatus.Create && o.OrderStatus != OrderStatus.Cancelled)
                .ToListAsync();
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
