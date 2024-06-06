using API.Models.DTOs.CustomerDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Interface for ProductService, defining operations related to products.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of products.</returns>
        /// <exception cref="UnableToDoActionException"> Thrown when unable to perform the action.</exception>
        Task<IEnumerable<ReturnCustomerSearchProductDto>> Get();

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product DTO.</returns>
        /// <exception cref="EntityNotFoundException{Product}">Thrown when the product is not found.</exception>
        /// <exception cref="UnableToDoActionException"> Thrown when unable to perform the action.</exception>
        Task<ReturnCustomerSearchProductDto> Get(int id);

        /// <summary>
        /// Searches for products based on search criteria.
        /// </summary>
        /// <param name="productSearchDto">The search criteria.</param>
        /// <returns>A collection of products matching the search criteria.</returns>
        /// <exception cref="ProductNotFoundException">Thrown when no products are found matching the search criteria.</exception>
        /// <exception cref="UnableToDoActionException"> Thrown when unable to perform the action.</exception>
        Task<IEnumerable<ReturnCustomerSearchProductDto>> Search(CustomerProductSearchDto productSearchDto);
    }
}
