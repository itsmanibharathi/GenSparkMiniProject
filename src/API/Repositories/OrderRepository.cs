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

        public override async Task<Order> GetAsync(int id)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Restaurant)
                .Include(x => x.Customer)
                .Include(x => x.CustomerAddress)
                .Include(x => x.Employee)
                .Include(x => x.CashPayment)
                .Include(x => x.OnlinePayment)
                .FirstOrDefaultAsync(x => x.OrderId == id && x.CustomerAddress.AddressId == x.CustomerAddressId);
        }

        //public virtual async Task<bool> IsDuplicate(Order entity)
        //{
        //    return await _context.Orders.AnyAsync(x => x.CustomerId == entity.CustomerId && x.OrderStatus == entity.OrderStatus && x.TotalAmount == entity.TotalAmount && x.OrderItems.Equals(entity.OrderItems));
        //}
    }
}
