using API.Models.DTOs.RestaurantDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Interface for RestaurantOrderService, defining operations related to restaurant orders.
    /// </summary>
    public interface IRestaurantOrderService
    {
        /// <summary>
        /// Retrieves a specific restaurant order by its ID.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <param name="orderId">The ID of the order to retrieve.</param>
        /// <returns>The restaurant order DTO.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the order does not belong to the specified restaurant.</exception>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="OrderNotFoundException">Thrown when the order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs while retrieving the order.</exception>
        public Task<ReturnRestaurantOrderDto> Get(int restaurantId, int orderId);

        /// <summary>
        /// Retrieves all today's orders for a restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <returns>A collection of today's orders for the restaurant.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs while updating the order.</exception>
        public Task<IEnumerable<ReturnRestaurantOrderDto>> Get(int restaurantId);

        /// <summary>
        /// Retrieves all orders for a restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <returns>A collection of all orders for the restaurant.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs while updating the order.</exception>
        public Task<IEnumerable<ReturnRestaurantOrderDto>> GetAll(int restaurantId);

        /// <summary>
        /// Updates the status of an order to preparing.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <param name="orderId">The ID of the order to update.</param>
        /// <returns>The updated restaurant order DTO.</returns>
        /// <exception cref="InvalidOrderException">Thrown when the order is not in place status.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the order does not belong to the specified restaurant.</exception>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs while updating the order.</exception>
        public Task<ReturnRestaurantOrderDto> PreparingOrder(int restaurantId, int orderId);

        /// <summary>
        /// Updates the status of an order to prepared.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <param name="orderId">The ID of the order to update.</param>
        /// <returns>The updated restaurant order DTO.</returns>
        /// <exception cref="InvalidOrderException">Thrown when the order is not in place status.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the order does not belong to the specified restaurant.</exception>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs while updating the order.</exception>
        public Task<ReturnRestaurantOrderDto> PreparedOrder(int restaurantId, int orderId);
    }
}
