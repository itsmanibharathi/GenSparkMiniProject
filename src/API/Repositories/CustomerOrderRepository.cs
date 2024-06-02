using API.Context;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CustomerOrderRepository : OrderRepository, ICustomerOrderRepository
    {
        public CustomerOrderRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId)
        {
            return await _context.Orders.Where(x => x.CustomerId == customerId).ToListAsync();
        }
    }
    
}
