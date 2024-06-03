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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.RestaurantControllers
{
    /// <summary>
    /// Controller for restaurant product operations.
    /// </summary>
    [Authorize(Policy = "RestaurantPolicy")]
    [Route("api/Restaurant/Product")]
    [ApiController]
    public class RestaurantProductController : ControllerBase
    {
        private readonly IRestaurantProductService _restaurantProductService;
        private readonly ILogger<RestaurantProductController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantProductController"/> class.
        /// </summary>
        /// <param name="restaurantProductService">The restaurant product service.</param>
        /// <param name="logger">The logger.</param>
        public RestaurantProductController(IRestaurantProductService restaurantProductService, ILogger<RestaurantProductController> logger)
        {
            _restaurantProductService = restaurantProductService;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new product for the authenticated restaurant.
        /// </summary>
        /// <param name="restaurantProductDto">The product DTO.</param>
        /// <returns>The added product.</returns>
        /// <response code="201">Returns the added product.</response>
        /// <response code="409">If the product already exists.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(ApiResponse<ReturnRestaurantProductDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Add(RestaurantProductDto restaurantProductDto)
        {
            try
            {
                restaurantProductDto.RestaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Adding product");
                var result = await _restaurantProductService.Add(restaurantProductDto);
                var response = new ApiResponse<ReturnRestaurantProductDto>(StatusCodes.Status201Created, result);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (EntityAlreadyExistsException<Product> ex)
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
        /// Updates an existing product for the authenticated restaurant.
        /// </summary>
        /// <param name="restaurantProductDto">The product DTO.</param>
        /// <returns>The updated product.</returns>
        /// <response code="200">Returns the updated product.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("update")]
        [ProducesResponseType(typeof( ApiResponse<ReturnRestaurantProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(RestaurantProductDto restaurantProductDto)
        {
            try
            {
                restaurantProductDto.RestaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Updating product");
                var result = await _restaurantProductService.Update(restaurantProductDto);
                var response = new ApiResponse<ReturnRestaurantProductDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Gets a product by product ID for the authenticated restaurant.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>The product.</returns>
        /// <response code="200">Returns the product.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{productId}")]
        [ProducesResponseType(typeof( ReturnRestaurantProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(int productId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting product");
                var result = await _restaurantProductService.Get(restaurantId, productId);
                var response = new ApiResponse<ReturnRestaurantProductDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Gets all products for the authenticated restaurant.
        /// </summary>
        /// <returns>The list of products.</returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="404">If no products are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnRestaurantProductDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting all products");
                var result = await _restaurantProductService.GetAll(restaurantId);
                var response = new ApiResponse<IEnumerable<ReturnRestaurantProductDto>>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Sets a product as available for the authenticated restaurant.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>The updated product.</returns>
        /// <response code="200">Returns the updated product.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="409">If the product is already up-to-date.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("available/{productId}")]
        [ProducesResponseType(typeof( ApiResponse<ReturnRestaurantProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Available(int productId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Making product available");
                var result = await _restaurantProductService.Available(restaurantId, productId);
                var response = new ApiResponse<ReturnRestaurantProductDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (AlreadyUptoDateException ex)
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
        /// Sets a product as unavailable for the authenticated restaurant.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>The updated product.</returns>
        /// <response code="200">Returns the updated product.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="409">If the product is already up-to-date.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("unavailable/{productId}")]
        [ProducesResponseType(typeof( ApiResponse<ReturnRestaurantProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UnAvailable(int productId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Making product unavailable");
                var result = await _restaurantProductService.UnAvailable(restaurantId, productId);
                var response = new ApiResponse<ReturnRestaurantProductDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (AlreadyUptoDateException ex)
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
        /// Deletes a product for the authenticated restaurant.
        /// </summary>
        /// <param name="productId">The product ID.</param>
        /// <returns>A success message.</returns>
        /// <response code="200">Returns a success message.</response>
        /// <response code="404">If the product is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpDelete("delete/{productId}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int productId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Deleting product");
                var res = await _restaurantProductService.Delete(restaurantId, productId);
                var response = new ApiResponse(StatusCodes.Status200OK, "Product deleted successfully");
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
