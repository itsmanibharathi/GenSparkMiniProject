using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.EmployeeDto;
using API.Models.Enums;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers.EmployeeControllers
{
    /// <summary>
    /// Controller to manage employee orders.
    /// </summary>
    [Route("api/employee/order")]
    [Authorize(Policy = "EmployeePolicy")]
    [ApiController]
    public class EmployeeOrderContollers : ControllerBase
    {
        private IEmployeeOrderService _employeeOrderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeOrderContollers"/> class.
        /// </summary>
        /// <param name="employeeOrderService">The service for handling employee orders.</param>
        public EmployeeOrderContollers(IEmployeeOrderService employeeOrderService)
        {
            _employeeOrderService = employeeOrderService;
        }

        /// <summary>
        /// Gets all orders for the logged-in employee.
        /// </summary>
        /// <returns>The list of orders.</returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="404">If no orders are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnEmployeeOrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                int EmployeeId = int.Parse(User.FindFirstValue("Id"));
                var result = await _employeeOrderService.GetByEmpId(EmployeeId);
                var response = new ApiResponse<IEnumerable<ReturnEmployeeOrderDto>>(StatusCodes.Status200OK, result);
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
        /// Gets a specific order by its ID for the logged-in employee.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>The order.</returns>
        /// <response code="200">Returns the order.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnEmployeeOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int orderId)
        {
            try
            {
                var EmployeeId = int.Parse(User.Identity.Name);
                var result = await _employeeOrderService.Get(EmployeeId, orderId);
                var response = new ApiResponse<ReturnEmployeeOrderDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
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
        /// Searches orders for the logged-in employee.
        /// </summary>
        /// <returns>The list of orders matching the search criteria.</returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="404">If no orders are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnEmployeeOrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Search()
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                var result = await _employeeOrderService.Search(EmployeeId);
                var response = new ApiResponse<IEnumerable<ReturnEmployeeOrderDto>>(StatusCodes.Status200OK, result);
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
        /// Accepts an order by its ID for the logged-in employee.
        /// </summary>
        /// <param name="orderId">The ID of the order to accept.</param>
        /// <returns>The accepted order.</returns>
        /// <response code="200">Returns the accepted order.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPut("accept/{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnEmployeeOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult> AcceptOrder(int orderId)
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                var result = await _employeeOrderService.AcceptOrder(EmployeeId, orderId);
                var response = new ApiResponse<ReturnEmployeeOrderDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message));
            }
            catch (InvalidOrderException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Picks up an order by its ID for the logged-in employee.
        /// </summary>
        /// <param name="orderId">The ID of the order to pick up.</param>
        /// <returns>The picked-up order.</returns>
        /// <response code="200">Returns the picked-up order.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPut("pickup/{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnEmployeeOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult> PickUpOrder(int orderId)
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                var result = await _employeeOrderService.PickUpOrder(EmployeeId, orderId);
                var response = new ApiResponse<ReturnEmployeeOrderDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message));
            }
            catch (InvalidOrderException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }

        }

        /// <summary>
        /// Delivers an order by its ID for the logged-in employee.
        /// </summary>
        /// <param name="orderId">The ID of the order to deliver.</param>
        /// <param name="amount">The amount received for the order.</param>
        /// <returns>The delivered order.</returns>
        /// <response code="200">Returns the delivered order.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPut("deliver/{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnEmployeeOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeliverOrder(int orderId)
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                var result = await _employeeOrderService.DeliverOrder(EmployeeId, orderId);
                var response = new ApiResponse<ReturnEmployeeOrderDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ApiResponse(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message));
            }
            catch (InvalidOrderException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        /// <summary>
        /// Gets all orders for the logged-in employee.
        /// </summary>
        /// <returns>The list of orders.</returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="404">If no orders are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnEmployeeOrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                var result = await _employeeOrderService.GetAllByEmpId(EmployeeId);
                var response = new ApiResponse<IEnumerable<ReturnEmployeeOrderDto>>(StatusCodes.Status200OK, result);
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
    }
}
