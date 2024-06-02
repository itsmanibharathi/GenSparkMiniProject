using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Models.Enums;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers
{
    [Route("api/Customer/Order")]
    [Authorize(Policy = "CustomerPolicy")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _customerOrderService;
        private readonly ILogger<Controller> _logger;

        public CustomerOrderController(ICustomerOrderService customerOrderService, ILogger<Controller> logger)
        {
            _customerOrderService = customerOrderService;
            _logger = logger;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(IEnumerable<ReturnCustomerOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOrder(CustomerOrderDto createCustomerOrderDto)
        {
            try
            {
                createCustomerOrderDto.CustomerId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Creating order");
                return Ok(await _customerOrderService.CreateOrder(createCustomerOrderDto));
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (EntityNotFoundException<Customer> ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (EntityNotFoundException<Restaurant> ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (EntityAlreadyExistsException<Order> ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (ProductUnAvailableException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }

        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(ReturnCustomerOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetOrder(int orderId)
        {
            try
            {
                int customerId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting order");
                return Ok(await _customerOrderService.GetOrder(customerId, orderId));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReturnCustomerOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAllOrders()
        {
            try
            {
                int customerId = int.Parse(User.FindFirst("Id").Value);
                _logger.LogInformation("Getting all orders");
                return Ok(await _customerOrderService.GetAllOrders(customerId));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }


        [HttpPost("payment/online")]
        [ProducesResponseType(typeof(ReturnOrderOnlinePaymentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateOnlinePayment(CustomerOrderPaymentDto orderPaymentDto)
        {
            try
            {
                orderPaymentDto.CustomerId = int.Parse(User.FindFirst("Id").Value);
                orderPaymentDto.PaymentMethod = PaymentMethod.Online;
                _logger.LogInformation("Creating payment");
                return Ok(await _customerOrderService.CreateOnlinePayment(orderPaymentDto));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (InvalidOrderException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpPost("payment/COD")]
        [ProducesResponseType(typeof(IEnumerable<ReturnOrderCashPaymentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCashPayment(CustomerOrderPaymentDto orderPaymentDto)
        {
            try
            {
                orderPaymentDto.CustomerId = int.Parse(User.FindFirst("Id").Value);
                orderPaymentDto.PaymentMethod = PaymentMethod.COD;
                _logger.LogInformation("Creating payment");
                return Ok(await _customerOrderService.CreateCashPayment(orderPaymentDto));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (InvalidOrderException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }


    }
}
