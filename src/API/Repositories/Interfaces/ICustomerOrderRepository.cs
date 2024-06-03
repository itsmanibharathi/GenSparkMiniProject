using API.Models;

namespace API.Repositories.Interfaces
{
    /// <summary>
    /// Repository for managing customer orders.
    /// </summary>
    public interface ICustomerOrderRepository : IOrderRepository
    {
        /// <summary>
        /// Retrieves orders for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>A collection of orders.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when no orders are found for the customer.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to retrieve orders.</exception>
        public Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);
    }
}
