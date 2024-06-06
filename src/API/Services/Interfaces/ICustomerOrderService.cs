using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Interface for managing customer orders.
    /// </summary>
    public interface ICustomerOrderService
    {
        /// <summary>
        /// Creates orders for the customer.
        /// </summary>
        /// <param name="createCustomerOrderDto">The DTO containing data for creating customer orders.</param>
        /// <returns>A collection of DTOs representing the created customer orders.</returns>
        /// <exception cref="EntityNotFoundException{Restaurant}">Thrown when the restaurant is not found.</exception>
        /// <exception cref="EntityNotFoundException{Product}">Thrown when the product is not found.</exception>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer address is not found.</exception>
        /// <exception cref="ProductUnAvailableException">Thrown when the product is unavailable.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<IEnumerable<ReturnCustomerOrderDto>> CreateOrder(CustomerOrderDto createCustomerOrderDto);

        /// <summary>
        /// Retrieves all orders for a customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>A collection of DTOs representing the customer orders.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the customer does not have any orders.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<IEnumerable<ReturnCustomerOrderDto>> GetAllOrders(int customerId);

        /// <summary>
        /// Retrieves a specific order for a customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="orderId">The ID of the order to retrieve.</param>
        /// <returns>The DTO representing the customer order.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the user does not belong to the order.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        Task<ReturnCustomerOrderDto> GetOrder(int customerId, int orderId);

        /// <summary>
        /// Creates an online payment for customer orders.
        /// </summary>
        /// <param name="orderPaymentDto">The DTO containing data for creating the online payment.</param>
        /// <returns>The DTO representing the created online payment.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="InvalidOrderException">Thrown when the order is invalid for online payment.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<ReturnOrderOnlinePaymentDto> CreateOnlinePayment(CustomerOrderPaymentDto orderPaymentDto);

        /// <summary>
        /// Creates cash payments for customer orders.
        /// </summary>
        /// <param name="orderPaymentDto">The DTO containing data for creating the cash payment.</param>
        /// <returns>A collection of DTOs representing the created cash payments.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="InvalidOrderException">Thrown when the order is invalid for cash payment.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<IEnumerable<ReturnOrderCashPaymentDto>> CreateCashPayment(CustomerOrderPaymentDto orderPaymentDto);
    }
}
