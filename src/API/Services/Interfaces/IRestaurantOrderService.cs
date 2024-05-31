using API.Models.DTOs.RestaurantDto;

namespace API.Services.Interfaces
{
    public interface IRestaurantOrderService
    {
        public Task<ReturnRestaurantOrderDto> Get(int restaurantId, int orderId);
        public Task<IEnumerable<ReturnRestaurantOrderDto>> Get(int restaurantId);
        public Task<IEnumerable<ReturnRestaurantOrderDto>> GetAll(int restaurantId);
        public Task<ReturnRestaurantOrderDto> PreparingOrder(int restaurantId, int orderId);
        public Task<ReturnRestaurantOrderDto> PreparedOrder(int restaurantId, int orderId);
    }
}
