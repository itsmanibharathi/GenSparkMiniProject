using API.Context;
using API.Exceptions;
using API.Models;
using API.Models.Enums;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeOrderRepository : IEmployeeOrderRepository
    {
        readonly private DBGenSparkMinirojectContext _context;
        readonly OrderStatus[] statuses = { OrderStatus.Preparing,OrderStatus.Prepared,OrderStatus.PickedUp,OrderStatus.Delivered,OrderStatus.Cancelled };

        public EmployeeOrderRepository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> SearchOrder(List<AddressCode> employeeRange)
        {
            try
            {
                var orderstatus = new OrderStatus[] { OrderStatus.Preparing, OrderStatus.Prepared };
                var res = await _context.Orders.Include(o => o.CustomerAddress).Where(o => o.EmployeeId ==null &&  employeeRange.Contains(o.CustomerAddress.Code) && orderstatus.Contains(o.OrderStatus)).ToListAsync();
                return res.Count > 0 ? res : throw new OrderNotFoundException();
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to search orders", e);
            }
        }
        public async Task<Order> Get(int orderId)
        {
            try
            {
                return await _context.Orders.Include(o => o.CustomerAddress).FirstOrDefaultAsync(o => o.OrderId == orderId) ?? throw new OrderNotFoundException();
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

        public async Task<IEnumerable<Order>> GetAllByEmpId(int EmployeeId)
        {
            try
            {
                var res = await _context.Orders.Where(o => o.EmployeeId == EmployeeId && statuses.Contains(o.OrderStatus)).ToListAsync();   
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

        public async Task<IEnumerable<Order>> GetByEmpId(int employeeId)
        {
            try
            {
                var res = await _context.Orders
                    .Where(o => o.OrderDate.Date == DateTime.Today.Date && o.EmployeeId == employeeId&& statuses.Contains(o.OrderStatus)).ToListAsync();


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

        public async Task<Order> UpdateOrder(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                return await _context.SaveChangesAsync() > 0 ? order : throw new OrderNotFoundException();
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to update order", e);
            }
        }
    }
}
