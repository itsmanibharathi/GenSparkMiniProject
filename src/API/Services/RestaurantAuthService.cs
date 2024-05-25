using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class RestaurantAuthService : IRestaurantAuthService
    {
        private readonly IRepository<int, Restaurant> _restaurantRepository;
        private readonly IRestaurantAuthRepository _restaurantAuthRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Restaurant> _tokenService;
        private readonly IMapper _mapper;

        public RestaurantAuthService(IRepository<int, Restaurant> restaurantRepository, 
            IRestaurantAuthRepository restaurantAuthRepository,
            IPasswordHashService passwordHashService,
            ITokenService<Restaurant> tokenService, 
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _restaurantAuthRepository = restaurantAuthRepository;
            _passwordHashService = passwordHashService;
            _tokenService = tokenService;
            _mapper = mapper;

        }
        public async Task<ReturnRestaurantLoginDto> Login(RestaurantLoginDto restaurantLoginDto)
        {
            var restaurant = await _restaurantAuthRepository.Get(restaurantLoginDto.Email);
            if (_passwordHashService.Verify(restaurantLoginDto.Password ,restaurant.RestaurantAuth.Password))
            {
                var res = _mapper.Map<ReturnRestaurantLoginDto>(restaurant);
                res.Token = _tokenService.GenerateToken(restaurant);
                return res;
            }
            throw new InvalidUserCredentialException();
           
        }

        public Task<ReturnRestaurantRegisterDto> Register(RestaurantRegisterDto restaurantRegisterDto)
        {
            Restaurant restaurant = _mapper.Map<Restaurant>(restaurantRegisterDto);
            restaurant.RestaurantAuth = new RestaurantAuth
            {
                Password = _passwordHashService.Hash(restaurantRegisterDto.Password)
            };
            return _restaurantRepository.Add(restaurant).ContinueWith(t => _mapper.Map<ReturnRestaurantRegisterDto>(t.Result));
        }
    }
}
