using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly DBGenSparkMinirojectContext _context;
        public CustomerOrderRepository(DBGenSparkMinirojectContext context) 
        {
            _context = context;
        }
        public async Task<Order> Add(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                var res = await _context.SaveChangesAsync();
                return res > 0 ? order : throw new UnableToDoActionException("Unable to insert");
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Insert the new Order", ex);
            }
        }

        public async Task<Order> Get(int customerId, int orderId)
        {
            try
            {
                var res = await _context.Orders.SingleOrDefaultAsync(o => o.OrderId == orderId && o.CustomerId == customerId);
                return res ?? throw new OrderNotFoundException();
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Order", ex);
            }

        }

        public async Task<IEnumerable<Order>> GetAll(int customerId)
        {
            try
            {
                var res = await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
                return res.Count() > 0 ? res : throw new OrderNotFoundException();
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Orders", ex);
            }
        }
    }
}
