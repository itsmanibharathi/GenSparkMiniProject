//using API.Exceptions;
//using API.Models.DTOs;
//using API.Models.DTOs.RestaurantDto;
//using API.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers.RestaurantControllers
//{
//    [Authorize(Policy = "RestaurantPolicy")]
//    [Route("api/Restaurant/Order")]
//    [ApiController]
//    public class RestaurantOrderController : ControllerBase
//    {
//        private readonly IRestaurantOrderService _restaurantOrderService;

//        public RestaurantOrderController(IRestaurantOrderService restaurantOrderService)
//        {
//            _restaurantOrderService = restaurantOrderService;
//        }

//        [HttpGet("{orderId}")]
//        [ProducesResponseType(typeof(ReturnRestaurantOrderDto), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Get(int orderId)
//        {
//            try
//            {
//                int restaurantId = int.Parse(User.FindFirst("Id").Value);
//                return Ok(await _restaurantOrderService.Get(restaurantId, orderId));
//            }
//            catch (OrderNotFoundException ex)
//            {
//                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [HttpGet]
//        [ProducesResponseType(typeof(IEnumerable<ReturnRestaurantOrderDto>), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Get()
//        {
//            try
//            {
//                int restaurantId = int.Parse(User.FindFirst("Id").Value);
//                return Ok(await _restaurantOrderService.Get(restaurantId));
//            }
//            catch (OrderNotFoundException ex)
//            {
//                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [HttpGet("all")]
//        [ProducesResponseType(typeof(IEnumerable<ReturnRestaurantOrderDto>), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> GetAll()
//        {
//            try
//            {
//                int restaurantId = int.Parse(User.FindFirst("Id").Value);
//                return Ok(await _restaurantOrderService.GetAll(restaurantId));
//            }
//            catch (OrderNotFoundException ex)
//            {
//                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [HttpPost("{orderId}/Preparing")]
//        [ProducesResponseType(typeof(ReturnRestaurantOrderDto), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> PreparingOrder(int orderId)
//        {
//            try
//            {
//                int restaurantId = int.Parse(User.FindFirst("Id").Value);
//                return Ok(await _restaurantOrderService.PreparingOrder(restaurantId, orderId));
//            }
//            catch (OrderNotFoundException ex)
//            {
//                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }

//        [HttpPost("{orderId}/prepared")]
//        [ProducesResponseType(typeof(ReturnRestaurantOrderDto), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
//        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> PreparedOrder(int orderId)
//        {
//            try
//            {
//                int restaurantId = int.Parse(User.FindFirst("Id").Value);
//                return Ok(await _restaurantOrderService.PreparedOrder(restaurantId, orderId));
//            }
//            catch (OrderNotFoundException ex)
//            {
//                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//            }
//        }
//    }
//}
