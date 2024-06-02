using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IRestaurantProductRepository : IProductRepository
    {
        public Task<IEnumerable<Product>> GetByRestaurantIdAsync(int restaurantId);
    }
}
