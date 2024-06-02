using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.EmployeeDto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EmployeeControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ReturnEmployeeRegisterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register(EmployeeRegisterDto employeeRegisterDto)
        {
            try
            {
                _logger.LogInformation("Registering employee");
                return Ok(await _employeeService.Register(employeeRegisterDto));
            }
            catch (EntityAlreadyExistsException<Employee> ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status409Conflict, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ReturnEmployeeLoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(EmployeeLoginDto employeeLoginDto)
        {
            try
            {
                _logger.LogInformation("Logging in employee");
                return Ok(await _employeeService.Login(employeeLoginDto));
            }
            catch (InvalidUserCredentialException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new ErrorDto(StatusCodes.Status401Unauthorized, ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
