using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IRestaurantOrderRepository : IOrderRepository
    {
        public Task<IEnumerable<Order>> GetAllByRestaurantId(int restaurantId);
        public Task<IEnumerable<Order>> GetToDayByRestaurantId(int restaurantId);

    }
}
