﻿using API.Exceptions;
using API.Models.DTOs;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAuthService _customerAuthService;
        private readonly ILogger _logger;

        public CustomerController(ICustomerAuthService customerAuthService, ILogger<CustomerController> logger)
        {
            _customerAuthService = customerAuthService;
            _logger = logger;
        }

        [Authorize(policy: "CustomerPolicy")]
        [HttpPost("register")]
        //[ProducesResponseType]
        public async Task<ActionResult> Register(CustomerRegisterDto customerRegisterDto)
        {
            try
            {
                _logger.LogInformation("Registering customer");
                return Ok(await _customerAuthService.Regiser(customerRegisterDto));
            }
            catch(DataDuplicateException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound( new ErrorDto(StatusCodes.Status409Conflict, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerLoginDto customerLoginDto)
        {
            try
            {
                _logger.LogInformation("Logging in customer");
                return Ok(await _customerAuthService.Login(customerLoginDto));
            }
            catch (InvalidUserCredentialException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
