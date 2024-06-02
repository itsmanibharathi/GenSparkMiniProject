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

    [Authorize(Policy = "CustomerPolicy")]
    [Route("api/Customer/Address")]
    [ApiController]
    public class CustomerAddressController : ControllerBase
    {
        private readonly ICustomerAddressService _customerAddressService;
        private readonly ILogger<CustomerAddressController> _logger;

        public CustomerAddressController(ICustomerAddressService customerAddressService, ILogger<CustomerAddressController> logger)
        {
            _customerAddressService = customerAddressService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReturnCustomerAddressDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Add(CustomerAddressDto addCustomerAddressDto)
        {
            try
            {
                addCustomerAddressDto.CustomerId = int.Parse(User.FindFirst("Id").Value);
                var result = await _customerAddressService.Add(addCustomerAddressDto);
                return Ok(result);
            }
            catch (CultureNotFoundException ex)
            {
                _logger.LogWarning(ex, "Error in Add Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (EntityAlreadyExistsException<CustomerAddress> ex)
            {
                _logger.LogWarning(ex, "Error in Add Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status409Conflict, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Add Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpGet]

        [ProducesResponseType(typeof(ReturnCustomerAddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Get()
        {
            try
            {
                int CustomerId = int.Parse(User.FindFirst("Id").Value);
                var result = await _customerAddressService.Get(CustomerId);
                return Ok(result);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                _logger.LogWarning(ex, "Error in Get Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status409Conflict, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpGet("{CustomerAddressId}")]
        [ProducesResponseType(typeof(ReturnCustomerAddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get( int CustomerAddressId)
        {
            try
            {
                int CustomerId = int.Parse(User.FindFirst("Id").Value);
                var result = await _customerAddressService.Get(CustomerId, CustomerAddressId);
                return Ok(result);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                _logger.LogWarning(ex, "Error in Get Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Get Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [HttpDelete("{CustomerAddressId}")]
        [ProducesResponseType(typeof(ReturnCustomerAddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int CustomerAddressId)
        {
            try
            {
                int CustomerId = int.Parse(User.FindFirst("Id").Value);
                var result = await _customerAddressService.Delete(CustomerId, CustomerAddressId);
                return Ok(result);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                _logger.LogWarning(ex, "Error in Delete Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete Customer Address");
                return BadRequest(new ErrorDto(StatusCodes.Status500InternalServerError,ex.Message));
            }
        }
    }
}
