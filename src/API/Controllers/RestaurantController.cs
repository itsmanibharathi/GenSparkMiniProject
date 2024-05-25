using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantAuthService _restaurantAuthService;
        private ILogger<Restaurant> _logger;

        public RestaurantController(IRestaurantAuthService restaurantAuthService , ILogger<Restaurant> logger )
        {
            _restaurantAuthService = restaurantAuthService;
            _logger = logger;
        }
        //[Authorize(policy: "RestaurantPolicy")]
        [HttpPost("register")]
        [ProducesResponseType(typeof(ReturnRestaurantRegisterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(RestaurantRegisterDto restaurantRegisterDto)
        {
            try
            {
                _logger.LogInformation("Registering restaurant");
                return Ok(await _restaurantAuthService.Register(restaurantRegisterDto));
            }
            catch (DataDuplicateException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status409Conflict, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ReturnRestaurantLoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(RestaurantLoginDto restaurantLoginDto)
        {
            try
            {
                _logger.LogInformation("Logging in restaurant");
                return Ok(await _restaurantAuthService.Login(restaurantLoginDto));
            }
            catch (InvalidUserCredentialException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
