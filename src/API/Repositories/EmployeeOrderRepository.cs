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

        public override async Task<Order> GetAsync(int orderId)
        {
            try
            {
                return await _context.Orders
                    .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Product)
                    .Include(x => x.Employee)
                    .Include(x => x.CashPayment)
                    .FirstOrDefaultAsync(x => x.OrderId == orderId && statuses.Contains(x.OrderStatus)) ?? throw new EntityNotFoundException<Order>(orderId);
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Order", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetAllByEmployeeIdAsync(int employeeId)
        {
            try
            {
                var res = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Employee)
                .Include(x => x.CashPayment)
                .Where(x => x.EmployeeId == employeeId && statuses.Contains(x.OrderStatus)).ToListAsync();
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
                var orderstatus = new OrderStatus[] { OrderStatus.Preparing, OrderStatus.Prepared };
                var res = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Include(x => x.Employee)
                .Include(x => x.CashPayment)
                .Include(o => o.CustomerAddress).Where(o => o.EmployeeId == null && employeeRange.Contains(o.CustomerAddress.Code) && orderstatus.Contains(o.OrderStatus)).ToListAsync();
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
