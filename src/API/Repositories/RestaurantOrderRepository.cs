using API.Context;
using API.Exceptions;
using API.Models;
using API.Models.Enums;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RestaurantOrderRepository : IRestaurantOrderRepository
    {
        private DBGenSparkMinirojectContext _context;

        public RestaurantOrderRepository(DBGenSparkMinirojectContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> Get(int restaurantId)
        {
            try
            {
                var res= await _context.Orders.Where(o => o.RestaurantId == restaurantId && o.OrderDate.Date == DateTime.Today.Date).ToListAsync();
                return res.Count > 0 ? res : throw new OrderNotFoundException();
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get orders", e);
            }
        }

        public async Task<Order> Get(int restaurantId, int orderId)
        {
            try
            {
                return await _context.Orders.SingleOrDefaultAsync(o => o.RestaurantId == restaurantId && o.OrderId == orderId && o.OrderStatus != OrderStatus.Create)  ?? throw new OrderNotFoundException();
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get order", e);
            }
        }

        public async Task<IEnumerable<Order>> GetAll(int restaurantId)
        {
            try
            {
               var res = await _context.Orders.Where(o => o.RestaurantId == restaurantId && o.OrderStatus != OrderStatus.Create).ToListAsync();
                return res.Count > 0 ? res : throw new OrderNotFoundException();
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get orders", e);
            }
        }

        public async Task<Order> Update(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                return await _context.SaveChangesAsync() > 0 ? order : throw new UnableToDoActionException("Unable to update order");
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to update order", e);
            }
        }
    }
}
