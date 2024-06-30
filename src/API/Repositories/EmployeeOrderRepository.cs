using API.Context;
using API.Exceptions;
using API.Models;
using API.Models.Enums;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeOrderRepository : OrderRepository, IEmployeeOrderRepository
    {
        readonly OrderStatus[] statuses = { OrderStatus.Preparing, OrderStatus.Prepared, OrderStatus.PickedUp, OrderStatus.Delivered, OrderStatus.Cancelled };
        public EmployeeOrderRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAllByEmployeeIdAsync(int employeeId)
        {
            try
            {
                var res = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(o => o.Restaurant)
                .Include(o => o.CustomerAddress)
                .Include(x => x.OnlinePayment)
                .Include(x => x.CashPayment)
                .Where(x => x.EmployeeId == employeeId && statuses.Contains(x.OrderStatus) && x.CustomerAddress.AddressId == x.CustomerAddressId).ToListAsync();
                return res.Count > 0 ? res : throw new EntityNotFoundException<Order>(employeeId);
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Orders by Employee Id", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetTodayByEmployeeIdAsync(int employeeId)
        {
            try
            {
                var res = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(o => o.Restaurant)
                .Include(o => o.CustomerAddress)
                .Include(x => x.OnlinePayment)
                .Include(x => x.CashPayment)
                .Where(x => x.EmployeeId == employeeId && x.OrderDate.Date == DateTime.Now.Date && statuses.Contains(x.OrderStatus)).ToListAsync();
                return res.Count > 0 ? res : throw new EntityNotFoundException<Order>(employeeId);
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Orders by Employee Id", ex);
            }
        }

        public async Task<IEnumerable<Order>> SearchOrderAsync(List<AddressCode> employeeRange)
        {
            try
            {
                Console.WriteLine("Hello World!" + employeeRange);
                foreach (var item in employeeRange)
                {
                    Console.WriteLine(item);
                }
                var orderstatus = new OrderStatus[] { OrderStatus.Preparing, OrderStatus.Prepared };
                var res = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Restaurant)
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(o => o.CustomerAddress)
                .Include(x => x.OnlinePayment)
                .Include(x => x.CashPayment)
                .Where(o => o.EmployeeId == null && employeeRange.Contains(o.Restaurant.AddressCode) && orderstatus.Contains(o.OrderStatus)).ToListAsync();
                return res.Count > 0 ? res : throw new EntityNotFoundException<Order>();
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Orders by Employee Range", ex);
            }
        }
    }
}
