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
    /// Controller for restaurant order operations.
    /// </summary>
    [Authorize(Policy = "RestaurantPolicy")]
    [Route("api/Restaurant/Order")]
    [ApiController]
    public class RestaurantOrderController : ControllerBase
    {
        private readonly IRestaurantOrderService _restaurantOrderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantOrderController"/> class.
        /// </summary>
        /// <param name="restaurantOrderService">The restaurant order service.</param>
        public RestaurantOrderController(IRestaurantOrderService restaurantOrderService)
        {
            _restaurantOrderService = restaurantOrderService;
        }

        /// <summary>
        /// Gets a restaurant order by order ID.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The restaurant order.</returns>
        /// <response code="200">Returns the restaurant order.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnRestaurantOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int orderId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                var order = await _restaurantOrderService.Get(restaurantId, orderId);
                var respone = new ApiResponse<ReturnRestaurantOrderDto>(StatusCodes.Status200OK,  order);
                return StatusCode(StatusCodes.Status200OK, respone);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return NotFound(new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Gets all orders for the authenticated restaurant.
        /// </summary>
        /// <returns>The list of restaurant orders.</returns>
        /// <response code="200">Returns the list of restaurant orders.</response>
        /// <response code="404">If no orders are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnRestaurantOrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                var result = await _restaurantOrderService.Get(restaurantId);
                var response = new ApiResponse<IEnumerable<ReturnRestaurantOrderDto>>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Gets all orders for the authenticated restaurant.
        /// </summary>
        /// <returns>The list of restaurant orders.</returns>
        /// <response code="200">Returns the list of restaurant orders.</response>
        /// <response code="404">If no orders are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnRestaurantOrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                var result = await _restaurantOrderService.GetAll(restaurantId);
                var response = new ApiResponse<IEnumerable<ReturnRestaurantOrderDto>>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Sets the status of an order to "Preparing".
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The updated order.</returns>
        /// <response code="200">Returns the updated order.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPut("Preparing/{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnRestaurantOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PreparingOrder(int orderId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                var result = await _restaurantOrderService.PreparingOrder(restaurantId, orderId);
                var response = new ApiResponse<ReturnRestaurantOrderDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (InvalidOrderException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Sets the status of an order to "Prepared".
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The updated order.</returns>
        /// <response code="200">Returns the updated order.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPut("prepared/{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnRestaurantOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PreparedOrder(int orderId)
        {
            try
            {
                int restaurantId = int.Parse(User.FindFirst("Id").Value);
                var result = await _restaurantOrderService.PreparedOrder(restaurantId, orderId);
                var response = new ApiResponse<ReturnRestaurantOrderDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (InvalidOrderException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
