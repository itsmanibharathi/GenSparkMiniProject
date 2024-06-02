using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerAuthService;
        private readonly ILogger _logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerAuthService = customerService;
            _logger = logger;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ReturnCustomerRegisterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(CustomerRegisterDto customerRegisterDto)
        {
            try
            {
                _logger.LogInformation("Registering customer");
                return Ok(await _customerAuthService.Register(customerRegisterDto));
            }
            catch (EntityAlreadyExistsException<Customer> ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status409Conflict, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }

        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ReturnCustomerLoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(CustomerLoginDto customerLoginDto)
        {
            try
            {
                _logger.LogInformation("Logging in customer");
                return Ok(await _customerAuthService.Login(customerLoginDto));
            }
            catch (InvalidUserCredentialException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status401Unauthorized, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }
    }
}
