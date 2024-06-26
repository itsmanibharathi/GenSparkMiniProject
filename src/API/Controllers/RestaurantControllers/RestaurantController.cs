using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.RestaurantDto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Controllers.RestaurantControllers
{
    /// <summary>
    /// Controller for restaurant operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantAuthService _restaurantAuthService;
        private ILogger<RestaurantController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantController"/> class.
        /// </summary>
        /// <param name="restaurantAuthService">The restaurant authentication service.</param>
        /// <param name="logger">The logger.</param>
        public RestaurantController(IRestaurantAuthService restaurantAuthService, ILogger<RestaurantController> logger)
        {
            _restaurantAuthService = restaurantAuthService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new restaurant.
        /// </summary>
        /// <param name="restaurantRegisterDto">The restaurant registration data.</param>
        /// <returns>The registered restaurant data.</returns>
        /// <response code="200">Returns the registered restaurant data.</response>
        /// <response code="409">If the restaurant already exists.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<ReturnRestaurantRegisterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(RestaurantRegisterDto restaurantRegisterDto)
        {
            try
            {
                _logger.LogInformation("Registering restaurant");
                var restaurant = await _restaurantAuthService.Register(restaurantRegisterDto);
                var response = new ApiResponse<ReturnRestaurantRegisterDto>(StatusCodes.Status201Created, restaurant);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (EntityAlreadyExistsException<Restaurant> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status409Conflict, new ApiResponse(StatusCodes.Status409Conflict, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Logs in a restaurant.
        /// </summary>
        /// <param name="restaurantLoginDto">The restaurant login data.</param>
        /// <returns>The logged-in restaurant data.</returns>
        /// <response code="200">Returns the logged-in restaurant data.</response>
        /// <response code="401">If the login credentials are invalid.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<ReturnRestaurantLoginDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(RestaurantLoginDto restaurantLoginDto)
        {
            try
            {
                _logger.LogInformation("Logging in restaurant");
                var restaurant = await _restaurantAuthService.Login(restaurantLoginDto);
                var response = new ApiResponse<ReturnRestaurantLoginDto>(StatusCodes.Status200OK, restaurant);
                return StatusCode(StatusCodes.Status200OK, response);


            }
            catch (InvalidUserCredentialException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
