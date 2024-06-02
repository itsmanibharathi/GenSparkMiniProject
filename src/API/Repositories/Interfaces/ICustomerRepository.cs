using API.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<int, Customer>
    {
        public Task<Customer> GetByEmailId(string emailId);
    }
}
