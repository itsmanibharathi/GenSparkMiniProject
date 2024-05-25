using API.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomerAuthRepository
    {
        public Task<Customer> Get(string email);
        //public Task<Customer> Update(Customer customer); // for Update Password
    }
}
