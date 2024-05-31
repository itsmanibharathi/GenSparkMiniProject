using API.Models;

namespace API.Repositories.Interfaces
{
    public interface ICustomerOrderRepository
    {
        public Task<Order> Add(Order order);
        public Task<Order> Update(Order order);
        //public Task<Order> Delete(Order order);
        public Task<Order> Get( int customerId , int orderId);
        public Task<IEnumerable<Order>> Get( int customerId);
        Task AddOnlinePayment(OnlinePayment onlinePayment);
        Task AddCashPayment(CashPayment cashPayment);
    }
}
