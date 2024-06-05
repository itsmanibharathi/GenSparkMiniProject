using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Service for managing product operations.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _repository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of products.</returns>
        /// <exception cref="UnableToDoActionException"> Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnCustomerSearchProductDto>> Get()
        {
            try
            {
                var res = await _repository.GetAsync();
                return _mapper.Map<IEnumerable<ReturnCustomerSearchProductDto>>(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product DTO.</returns>
        /// <exception cref="EntityNotFoundException{Product}">Thrown when the product is not found.</exception>
        /// <exception cref="UnableToDoActionException"> Thrown when unable to perform the action.</exception>
        public async Task<ReturnCustomerSearchProductDto> Get(int id)
        {
            try
            {
                var res = await _repository.GetAsync(id);
                return _mapper.Map<ReturnCustomerSearchProductDto>(res);
            }
            catch (EntityNotFoundException<Product>)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Searches for products based on search criteria.
        /// </summary>
        /// <param name="productSearchDto">The search criteria.</param>
        /// <returns>A collection of products matching the search criteria.</returns>
        /// <exception cref="ProductNotFoundException">Thrown when no products are found matching the search criteria.</exception>
        /// <exception cref="UnableToDoActionException"> Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnCustomerSearchProductDto>> Search(CustomerProductSearchDto productSearchDto)
        {
            try
            {
                var res = await _repository.GetSearchAsync(productSearchDto);

                if (res.Any())
                {
                    return _mapper.Map<IEnumerable<ReturnCustomerSearchProductDto>>(res);
                }
                throw new ProductNotFoundException();
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
