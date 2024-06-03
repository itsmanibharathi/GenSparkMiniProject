using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace API.Controllers.CustomerControllers
{
    /// <summary>
    /// Controller to manage customer addresses.
    /// </summary>
    [Authorize(Policy = "CustomerPolicy")]
    [Route("api/Customer/Address")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerAddressService _customerAddressService;
        private readonly ILogger<CustomerAddressController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAddressController"/> class.
        /// </summary>
        /// <param name="customerAddressService">The service for handling customer addresses.</param>
        /// <param name="logger">The logger instance.</param>
        public CustomerAddressController(ICustomerAddressService customerAddressService, ILogger<CustomerAddressController> logger)
        {
            _customerAddressService = customerAddressService;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new customer address.
        /// </summary>
        /// <param name="addCustomerAddressDto">The customer address DTO.</param>
        /// <returns>The created customer address.</returns>
        /// <response code="200">Returns the created customer address.</response>
        /// <response code="400">If there is a validation error.</response>
        /// <response code="409">If the customer address already exists.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ReturnCustomerAddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CustomerAddressDto addCustomerAddressDto)
        {
            try
            {
                addCustomerAddressDto.CustomerId = int.Parse(User.FindFirst("Id").Value);
                var result = await _customerAddressService.Add(addCustomerAddressDto);
                var response = new ApiResponse<ReturnCustomerAddressDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (CultureNotFoundException ex)
            {
                _logger.LogWarning(ex, "Error in Add Customer Address");
                var response = new ApiResponse(StatusCodes.Status400BadRequest, ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, response);
            }
            catch (EntityAlreadyExistsException<CustomerAddress> ex)
            {
                _logger.LogWarning(ex, "Error in Add Customer Address");
                var response = new ApiResponse(StatusCodes.Status409Conflict, ex.Message);
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Add Customer Address");
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Gets all customer addresses for the logged-in customer.
        /// </summary>
        /// <returns>The list of customer addresses.</returns>
        /// <response code="200">Returns the list of customer addresses.</response>
        /// <response code="404">If no addresses are found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnCustomerAddressDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                int CustomerId = int.Parse(User.FindFirst("Id").Value);
                var result = await _customerAddressService.Get(CustomerId);
                var response = new ApiResponse<IEnumerable<ReturnCustomerAddressDto>>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                _logger.LogWarning(ex, "Error in Get Customer Address");
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Customer Address");
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Gets a specific customer address by its ID.
        /// </summary>
        /// <param name="CustomerAddressId">The ID of the customer address.</param>
        /// <returns>The customer address.</returns>
        /// <response code="200">Returns the customer address.</response>
        /// <response code="404">If the address is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{CustomerAddressId}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnCustomerAddressDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int CustomerAddressId)
        {
            try
            {
                int CustomerId = int.Parse(User.FindFirst("Id").Value);
                var result = await _customerAddressService.Get(CustomerId, CustomerAddressId);
                var response = new ApiResponse<ReturnCustomerAddressDto>(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                _logger.LogWarning(ex, "Error in Get Customer Address");
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Customer Address");
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Deletes a specific customer address by its ID.
        /// </summary>
        /// <param name="CustomerAddressId">The ID of the customer address.</param>
        /// <returns>An action result.</returns>
        /// <response code="200">If the address is successfully deleted.</response>
        /// <response code="404">If the address is not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpDelete("{CustomerAddressId}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int CustomerAddressId)
        {
            try
            {
                int CustomerId = int.Parse(User.FindFirst("Id").Value);
                var result = await _customerAddressService.Delete(CustomerId, CustomerAddressId);
                var response = new ApiResponse(StatusCodes.Status200OK, result);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                _logger.LogWarning(ex, "Error in Delete Customer Address");
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete Customer Address");
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
