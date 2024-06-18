using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace API.Controllers.CustomerControllers
{
    /// <summary>
    /// Controller to manage customer operations like registration and login.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerAuthService;
        private readonly ILogger<CustomerController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController"/> class.
        /// </summary>
        /// <param name="customerService">The customer service.</param>
        /// <param name="logger">The logger instance.</param>
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerAuthService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new customer.
        /// </summary>
        /// <param name="customerRegisterDto">The customer registration DTO.</param>
        /// <returns>The registered customer details.</returns>
        /// <response code="200">Returns the registered customer details.</response>
        /// <response code="409">If a customer with the same email or username already exists.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponse<ReturnCustomerRegisterDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(CustomerRegisterDto customerRegisterDto)
        {
            try
            {
                _logger.LogInformation("Registering customer");
                var res = await _customerAuthService.Register(customerRegisterDto);
                var response = new ApiResponse<ReturnCustomerRegisterDto>(StatusCodes.Status200OK, res);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (EntityAlreadyExistsException<Customer> ex)
            {
                _logger.LogWarning(ex.Message);
                var response = new ApiResponse(StatusCodes.Status409Conflict, ex.Message);
                return StatusCode(StatusCodes.Status409Conflict, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Logs in a customer.
        /// </summary>
        /// <param name="customerLoginDto">The customer login DTO.</param>
        /// <returns>The logged-in customer details.</returns>
        /// <response code="200">Returns the logged-in customer details.</response>
        /// <response code="401">If the provided credentials are invalid.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponse<ReturnCustomerLoginDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(CustomerLoginDto customerLoginDto)
        {
            try
            {
                _logger.LogInformation("Logging in customer");
                var res = await _customerAuthService.Login(customerLoginDto);
                var response = new ApiResponse<ReturnCustomerLoginDto>(StatusCodes.Status200OK, res);
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (InvalidUserCredentialException ex)
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
    }
}
