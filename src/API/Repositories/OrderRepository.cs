using API.Context;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class OrderRepository : Repository<int, Order>, IOrderRepository
    {
        public OrderRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        //public virtual async Task<bool> IsDuplicate(Order entity)
        //{
        //    return await _context.Orders.AnyAsync(x => x.CustomerId == entity.CustomerId && x.OrderStatus == entity.OrderStatus && x.TotalAmount == entity.TotalAmount && x.OrderItems.Equals(entity.OrderItems));
        //}
    }
}
