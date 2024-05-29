using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface IRestaurantProductService
    {
        public Task<ReturnRestaurantProductDto> Add(RestaurantProductDto restaurantProductDto);
        public Task<ReturnRestaurantProductDto> Update(RestaurantProductDto restaurantProductDto);
        public Task<IEnumerable<ReturnRestaurantProductDto>> GetAll(int restaurantId);
        public Task<ReturnRestaurantProductDto> Get(int restaurantId, int productId);
        //public Task<IEnumerable<RestaurantProductDto>> GetAll();
        public Task<ReturnRestaurantProductDto> Available(int restaurantId, int productId);
        public Task<ReturnRestaurantProductDto> UnAvailable(int restaurantId, int productId);
        public Task<bool> Delete(int restaurantId, int productId);
    }
}
