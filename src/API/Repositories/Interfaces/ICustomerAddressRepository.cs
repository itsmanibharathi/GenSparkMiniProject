using API.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomerAddressRepository
    {
        public Task<CustomerAddress> Get(int CustomerId, int CustomerAddressId);
        public Task<CustomerAddress> Get(int AddressId);
        public Task<IEnumerable<CustomerAddress>> GetByCustomerId(int CustomerId);
        public Task<CustomerAddress> Add(CustomerAddress customerAddress);
        public Task<bool> Delete(int id);
    }
}
