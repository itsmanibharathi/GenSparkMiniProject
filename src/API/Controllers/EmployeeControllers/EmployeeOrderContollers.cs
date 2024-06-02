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

    [Route("api/employee/order")]
    [Authorize(Policy = "EmployeePolicy")]
    [ApiController]
    public class EmployeeOrderContollers : ControllerBase
    {
        private IEmployeeOrderService _employeeOrderService;

        public EmployeeOrderContollers(IEmployeeOrderService employeeOrderService)
        {
            _employeeOrderService = employeeOrderService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReturnEmployeeOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                int EmployeeId = int.Parse(User.FindFirstValue("Id"));
                return Ok(await _employeeOrderService.GetByEmpId(EmployeeId));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(ReturnEmployeeOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(int orderId)
        {
            try
            {
                var EmployeeId = int.Parse(User.Identity.Name);
                return Ok(await _employeeOrderService.Get(EmployeeId, orderId));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ReturnEmployeeOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> search()
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                return Ok(await _employeeOrderService.Search(EmployeeId));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("accept/{orderId}")]
        [ProducesResponseType(typeof(ReturnEmployeeOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AcceptOrder(int orderId)
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                return Ok(await _employeeOrderService.AcceptOrder(EmployeeId, orderId));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("pickup/{orderId}")]
        [ProducesResponseType(typeof(ReturnEmployeeOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PicUpOrder(int orderId)
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                return Ok(await _employeeOrderService.PicUpOrder(EmployeeId, orderId)); // ask this
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("deliver/{orderId}")]
        [ProducesResponseType(typeof(ReturnEmployeeOrderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeliverOrder(int orderId, decimal? amount)
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                return Ok(await _employeeOrderService.DeliverOrder(EmployeeId, orderId, amount)); // ask this
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ReturnEmployeeOrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var EmployeeId = int.Parse(User.FindFirst("Id").Value);
                return Ok(await _employeeOrderService.GetAllByEmpId(EmployeeId));
            }
            catch (EntityNotFoundException<Order> ex)
            {
                return NotFound(new ErrorDto(StatusCodes.Status404NotFound, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
