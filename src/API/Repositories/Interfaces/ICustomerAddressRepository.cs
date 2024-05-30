using API.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomerAddressRepository
    {
        /// <summary>
        /// Add Customer Address
        /// </summary>
        /// <param name="customerAddress"></param>
        /// <returns></returns>
        public Task<CustomerAddress> Add(CustomerAddress customerAddress);
        

        public Task<CustomerAddress> Get(int CustomerId, int CustomerAddressId);

        /// <summary>
        /// Get all the Customer Address by Customer Id
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public Task<IEnumerable<CustomerAddress>> Get(int CustomerId);
        
        public Task<bool> Delete(int CustomerId, int CustomerAddressId);
    }
}
