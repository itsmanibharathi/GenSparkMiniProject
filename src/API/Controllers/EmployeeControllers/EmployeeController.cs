using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.EmployeeDto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Controllers.EmployeeControllers
{
    /// <summary>
    /// Controller for managing employee operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="employeeService">The employee service.</param>
        /// <param name="logger">The logger.</param>
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new employee.
        /// </summary>
        /// <param name="employeeRegisterDto">The employee registration data.</param>
        /// <returns>The registered employee.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(ReturnEmployeeRegisterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(EmployeeRegisterDto employeeRegisterDto)
        {
            try
            {
                _logger.LogInformation("Registering employee");
                var result = await _employeeService.Register(employeeRegisterDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (EntityAlreadyExistsException<Employee> ex)
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
        /// Logs in an employee.
        /// </summary>
        /// <param name="employeeLoginDto">The employee login credentials.</param>
        /// <returns>The logged in employee.</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ReturnEmployeeLoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(EmployeeLoginDto employeeLoginDto)
        {
            try
            {
                _logger.LogInformation("Logging in employee");
                var result = await _employeeService.Login(employeeLoginDto);
                return StatusCode(StatusCodes.Status200OK, result);
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
