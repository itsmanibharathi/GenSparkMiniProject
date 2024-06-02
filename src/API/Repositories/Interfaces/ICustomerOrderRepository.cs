using API.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomerOrderRepository : IOrderRepository
    {
        public Task<IEnumerable<Order>> GetOrdersByCustomerId(int customerId);
    }
}
