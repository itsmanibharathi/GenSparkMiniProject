using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    /// <summary>
    /// Repository for managing customer orders.
    /// </summary>
    public class CustomerOrderRepository : OrderRepository, ICustomerOrderRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CustomerOrderRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves orders for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>A collection of orders.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when no orders are found for the customer.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to retrieve orders.</exception>
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            try
            {
                var res = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Restaurant)
                .Where(x => x.CustomerId == customerId).ToListAsync();
                return res.Count > 0 ? res : throw new EntityNotFoundException<Order>();
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException($"Unable to retrieve orders for customer {customerId}", ex);
            }
        }
    }
}
