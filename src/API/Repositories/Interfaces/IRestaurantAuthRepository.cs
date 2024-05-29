using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IRestaurantAuthRepository
    {
        public Task<Restaurant> Get(string email);
    }
}
