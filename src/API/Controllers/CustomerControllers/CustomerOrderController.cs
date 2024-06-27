using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Models.Enums;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers.CustomerControllers
{
    /// <summary>
    /// Controller to manage customer orders and payments.
    /// </summary>
    [Route("api/Customer/Order")]
    [Authorize(Policy = "CustomerPolicy")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;
        private readonly ILogger<CustomerOrderController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerOrderController"/> class.
        /// </summary>
        /// <param name="customerOrderService">The customer order service.</param>
        /// <param name="logger">The logger instance.</param>
        public CustomerOrderController(ICustomerOrderService customerOrderService, ILogger<CustomerOrderController> logger)
        {
            _customerOrderService = customerOrderService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new order for the customer.
        /// </summary>
        /// <param name="createCustomerOrderDto">The order details.</param>
        /// <returns>The created order details.</returns>
        /// <response code="200">Returns the created order details.</response>
        /// <response code="400">If the product is unavailable.</response>
        /// <response code="404">If the product, customer, or restaurant is not found.</response>
        /// <response code="409">If an order already exists.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("create")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnCustomerOrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrder(CustomerOrderDto createCustomerOrderDto)
        {
            try
            {
                createCustomerOrderDto.CustomerId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Creating order");
                var result = await _customerOrderService.CreateOrder(createCustomerOrderDto);
                var response = new ApiResponse<IEnumerable<ReturnCustomerOrderDto>>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (EntityNotFoundException<Customer> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (EntityNotFoundException<Restaurant> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (EntityAlreadyExistsException<Order> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status409Conflict, ex.Message);
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            catch (ProductUnAvailableException ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Gets a specific order of the customer.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The order details.</returns>
        /// <response code="200">Returns the order details.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="401">Order id is not belogs to user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnCustomerOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetOrder(int orderId)
        {
            try
            {
                int customerId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting order");
                var result = await _customerOrderService.GetOrder(customerId, orderId);
                var response = new ApiResponse<ReturnCustomerOrderDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex.Message);
                var response = new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message);
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Gets all orders of the customer.
        /// </summary>
        /// <returns>The list of orders.</returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="404">If no orders are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnCustomerOrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllOrders()
        {
            try
            {
                int customerId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting all orders");
                var result = await _customerOrderService.GetAllOrders(customerId);
                var response = new ApiResponse<IEnumerable<ReturnCustomerOrderDto>>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Gets all orders of the customer.
        /// </summary>
        /// <returns>The list of orders.</returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="404">If no orders are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("lastCreate")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnCustomerOrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetlastCreate()
        {
            try
            {
                int customerId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting all orders");
                var result = await _customerOrderService.GetlastCreate(customerId);
                var response = new ApiResponse<IEnumerable<ReturnCustomerOrderDto>>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Creates an online payment for the customer order.
        /// </summary>
        /// <param name="orderPaymentDto">The payment details.</param>
        /// <returns>The created online payment details.</returns>
        /// <response code="200">Returns the created online payment details.</response>
        /// <response code="400">If the order is invalid.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="401">Order id is not belogs to user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("payment/online")]
        [ProducesResponseType(typeof(ApiResponse<ReturnOrderOnlinePaymentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOnlinePayment(CustomerOrderPaymentDto orderPaymentDto)
        {
            try
            {
                orderPaymentDto.CustomerId = int.Parse(User.FindFirst("Id").Value);
                orderPaymentDto.PaymentMethod = PaymentMethod.Online;
                _logger.LogInformation("Creating online payment");
                var result = await _customerOrderService.CreateOnlinePayment(orderPaymentDto);
                var response = new ApiResponse<ReturnOrderOnlinePaymentDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex.Message);
                var response = new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message);
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (InvalidOrderException ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status400BadRequest, ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Creates a cash on delivery (COD) payment for the customer order.
        /// </summary>
        /// <param name="orderPaymentDto">The payment details.</param>
        /// <returns>The created COD payment details.</returns>
        /// <response code="200">Returns the created COD payment details.</response>
        /// <response code="400">If the order is invalid.</response>
        /// <response code="401">Order id is not belogs to user.</response>
        /// <response code="404">If the order is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("payment/COD")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnOrderCashPaymentDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCashPayment(CustomerOrderPaymentDto orderPaymentDto)
        {
            try
            {
                orderPaymentDto.CustomerId = int.Parse(User.FindFirst("Id").Value);
                orderPaymentDto.PaymentMethod = PaymentMethod.COD;
                _logger.LogInformation("Creating cash payment");
                var result = await _customerOrderService.CreateCashPayment(orderPaymentDto);
                var response = new ApiResponse<IEnumerable<ReturnOrderCashPaymentDto>>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex.Message);
                var response = new ApiResponse(StatusCodes.Status401Unauthorized, ex.Message);
                return StatusCode(StatusCodes.Status401Unauthorized, response);
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (InvalidOrderException ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status400BadRequest, ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}

