using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IRestaurantOrderRepository
    {
        public Task<IEnumerable<Order>> Get(int restaurantId);
        public Task<Order> Get(int restaurantId, int orderId);
        public Task<IEnumerable<Order>> GetAll(int restaurantId);
        public Task<Order> Update(Order order);
    }
}
