using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.RestaurantDto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers.RestaurantControllers
{
    [Authorize(Policy = "RestaurantPolicy")]
    [Route("api/Restaurant/Product")]
    [ApiController]
    public class RestaurantProductController : ControllerBase
    {
        private readonly IRestaurantProductService _restaurantProductService;
        private ILogger<RestaurantProductController> _logger;

        public RestaurantProductController(IRestaurantProductService restaurantProductService, ILogger<RestaurantProductController> logger)
        {
            _restaurantProductService = restaurantProductService;
            _logger = logger;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(ReturnRestaurantProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Add(RestaurantProductDto restaurantProductDto)
        {
            try
            {
                restaurantProductDto.RestaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Adding product");
                return Ok(await _restaurantProductService.Add(restaurantProductDto));
            }
            catch (EntityAlreadyExistsException<Product> ex)
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

        [HttpPost("Edite")]
        [ProducesResponseType(typeof(ReturnRestaurantProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(RestaurantProductDto restaurantProductDto)
        {
            try
            {
                restaurantProductDto.RestaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Updating product");
                return Ok(await _restaurantProductService.Update(restaurantProductDto));
            }
            catch (EntityNotFoundException<Product> ex)
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

        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ReturnRestaurantProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(int productId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting product");
                return Ok(await _restaurantProductService.Get(restaurantId, productId));
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ReturnRestaurantProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting all products");
                return Ok(await _restaurantProductService.GetAll(restaurantId));
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("available/{productId}")]
        [ProducesResponseType(typeof(ReturnRestaurantProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Available(int productId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Making product available");
                return Ok(await _restaurantProductService.Available(restaurantId, productId));
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch(AlreadyUptoDateException ex)
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

        [HttpPost("unavailable/{productId}")]
        [ProducesResponseType(typeof(ReturnRestaurantProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UnAvailable(int productId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Making product unavailable");
                return Ok(await _restaurantProductService.UnAvailable(restaurantId, productId));
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (AlreadyUptoDateException ex)
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

        [HttpDelete("delete/{productId}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int productId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Deleting product");
                var res=  await _restaurantProductService.Delete(restaurantId, productId);
                return Ok(new SuccessDto("Product Deleted successfully"));
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
