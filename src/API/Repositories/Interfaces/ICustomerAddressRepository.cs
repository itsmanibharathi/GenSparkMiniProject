using API.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomerAddressRepository : IRepository<int, CustomerAddress>
    {
        /// <summary>
        /// Gets all addresses for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>A collection of customer addresses.</returns>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when no addresses are found for the customer.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to retrieve customer addresses.</exception>

        public Task<IEnumerable<CustomerAddress>> GetByCustomerIdAsync(int customerId);

        /// <summary>
        /// Deletes a customer address by customer ID and address ID.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <param name="customerAddressId">The customer address ID.</param>
        /// <returns>True if the delete operation was successful, false otherwise.</returns>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer address is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to delete the customer address.</exception>
        public Task<bool> DeleteAsync(int customerId, int customerAddressId);
    }
}
