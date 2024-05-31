﻿using API.Context;
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

        public Task AddCashPayment(CashPayment cashPayment)
        {
            try
            {
                _context.CashPayments.Add(cashPayment);
                var res = _context.SaveChangesAsync();
                return res.Result > 0 ? Task.CompletedTask : throw new UnableToDoActionException("Unable to insert");
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Insert the new Cash Payment", ex);
            }
        }

        public Task AddOnlinePayment(OnlinePayment onlinePayment)
        {
            try
            {
                _context.OnlinePayments.Add(onlinePayment);
                var res = _context.SaveChangesAsync();
                return res.Result > 0 ? Task.CompletedTask : throw new UnableToDoActionException("Unable to insert");
            }
                catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Insert the new Online Payment", ex);
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

        public async Task<IEnumerable<Order>> Get(int customerId)
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

        public Task<Order> Update(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                var res = _context.SaveChangesAsync();
                return res.Result > 0 ? Task.FromResult(order) : throw new UnableToDoActionException("Unable to update");
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to update the Order", ex);
            }
        }
    }
}
