using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IRestaurantRepository : IRepository<int, Restaurant>
    {
        public Task<Restaurant> GetByEmailId(string emailId);
    }
}
