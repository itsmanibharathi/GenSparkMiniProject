using API.Exceptions;
using API.Models;
using API.Models.DTOs.RestaurantDto;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class RestaurantAuthService : IRestaurantAuthService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Restaurant> _tokenService;
        private readonly IMapper _mapper;

        public RestaurantAuthService(
            IRestaurantRepository restaurantRepository,
            IPasswordHashService passwordHashService,
            ITokenService<Restaurant> tokenService,
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            _passwordHashService = passwordHashService;
            _tokenService = tokenService;
            _mapper = mapper;

        }
        public async Task<ReturnRestaurantLoginDto> Login(RestaurantLoginDto restaurantLoginDto)
        {
            try
            {
                var restaurant = await _restaurantRepository.GetByEmailId(restaurantLoginDto.Email);
                if (_passwordHashService.Verify(restaurantLoginDto.Password, restaurant.RestaurantAuth.Password))
                {
                    var res = _mapper.Map<ReturnRestaurantLoginDto>(restaurant);
                    res.Token = _tokenService.GenerateToken(restaurant);
                    return res;
                }
                throw new InvalidUserCredentialException();
            }
            catch (EntityNotFoundException<Restaurant> )
            {
                throw new InvalidUserCredentialException();
            }
            catch(InvalidUserCredentialException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException($"Unable to login restaurant with email {restaurantLoginDto.Email}",ex);
            }


        }

        public async Task<ReturnRestaurantRegisterDto> Register(RestaurantRegisterDto restaurantRegisterDto)
        {
            try
            {
                Restaurant restaurant = _mapper.Map<Restaurant>(restaurantRegisterDto);
                restaurant.RestaurantAuth = new RestaurantAuth
                {
                    Password = _passwordHashService.Hash(restaurantRegisterDto.Password)
                };
                var res = await _restaurantRepository.AddAsync(restaurant);
                return _mapper.Map<ReturnRestaurantRegisterDto>(res);
            }
            catch (EntityAlreadyExistsException<Restaurant>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to register restaurant", ex);
            }
        }
    }
}
