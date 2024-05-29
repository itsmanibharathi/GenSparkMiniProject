using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface IRestaurantAuthService
    {
        public Task<ReturnRestaurantRegisterDto> Register(RestaurantRegisterDto restaurantRegisterDto);
        public Task<ReturnRestaurantLoginDto> Login(RestaurantLoginDto restaurantLoginDto);

    }
}
