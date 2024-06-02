using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    /// <summary>
    /// Repository for managing customer addresses.
    /// </summary>
    public class CustomerAddressRespository : Repository<int, CustomerAddress>, ICustomerAddressRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAddressRespository"/> class.
        /// </summary>
        /// <param name="dataContext">The database context.</param>
        public CustomerAddressRespository(DBGenSparkMinirojectContext dataContext) : base(dataContext)
        {
        }

        /// <summary>
        /// Deletes a customer address by customer ID and address ID.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <param name="customerAddressId">The customer address ID.</param>
        /// <returns>True if the delete operation was successful, false otherwise.</returns>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer address is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to delete the customer address.</exception>
        public async Task<bool> DeleteAsync(int customerId, int customerAddressId)
        {
            try
            {
                var res = await GetAsync(customerAddressId);
                if (res.CustomerId == customerId)
                {
                    _context.CustomerAddresses.Remove(res);
                    return await _context.SaveChangesAsync() > 0;
                }
                throw new EntityNotFoundException<CustomerAddress>(customerAddressId);
            }
            catch (EntityNotFoundException<CustomerAddress>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to delete the Customer {customerId} Address", e);
            }
        }

        /// <summary>
        /// Gets all addresses for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>A collection of customer addresses.</returns>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when no addresses are found for the customer.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to retrieve customer addresses.</exception>
        public async Task<IEnumerable<CustomerAddress>> GetByCustomerIdAsync(int customerId)
        {
            try
            {
                var res = await _context.CustomerAddresses.Where(c => c.CustomerId == customerId).ToListAsync();
                return res.Count > 0 ? res : throw new EntityNotFoundException<CustomerAddress>(customerId);
            }
            catch (EntityNotFoundException<CustomerAddress>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to get the Customer {customerId} Address", e);
            }
        }

        /// <summary>
        /// Checks if a customer address is duplicate.
        /// </summary>
        /// <param name="customerAddress">The customer address to check.</param>
        /// <returns>True if the customer address is duplicate, false otherwise.</returns>
        public override async Task<bool> IsDuplicate(CustomerAddress customerAddress)
        {
            return await _context.CustomerAddresses.AnyAsync(c => c.CustomerId == customerAddress.CustomerId && c.Code == customerAddress.Code && c.Type == customerAddress.Type);
        }
    }
}
