using API.Models;

namespace API.Interfaces
{
    public interface ICustomerService
    {
        public Task<Customer> Regiser(Customer customer);
    }
}
