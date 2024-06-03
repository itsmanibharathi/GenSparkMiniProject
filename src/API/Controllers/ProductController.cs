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
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnCustomerSearchProductDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Search([FromQuery] CustomerProductSearchDto productSearchDto)
        {
            try
            {
                var res = await _productService.Search(productSearchDto);
                var response = new ApiResponse<IEnumerable<ReturnCustomerSearchProductDto>>(StatusCodes.Status200OK, res);
                return Ok(response);

            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound, ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<ReturnCustomerSearchProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await _productService.Get(id);
                var response = new ApiResponse<ReturnCustomerSearchProductDto>(StatusCodes.Status200OK, res);
                return Ok(response);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                _logger.LogWarning(ex.Message);
                var response = new ApiResponse(StatusCodes.Status404NotFound,ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var response = new ApiResponse(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ReturnCustomerSearchProductDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _productService.Get();
                var response = new ApiResponse<IEnumerable<ReturnCustomerSearchProductDto>>(StatusCodes.Status200OK, res);
                return Ok(response);
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
