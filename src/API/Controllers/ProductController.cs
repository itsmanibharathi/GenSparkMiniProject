using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductSerivce _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductSerivce productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ReturnCustomerSearchProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Search([FromQuery] CustomerProductSearchDto productSearchDto)
        {
            try
            {
                var res = await _productService.Search(productSearchDto);
                return Ok(res);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReturnCustomerSearchProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await _productService.Get(id);
                return Ok(res);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ReturnCustomerSearchProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _productService.Get();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
