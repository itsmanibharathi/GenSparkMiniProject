using API.Models.DTOs.EmployeeDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Interface for employee order service operations.
    /// </summary>
    public interface IEmployeeOrderService
    {
        /// <summary>
        /// Searches for employee orders within a circular range of addresses assigned to the employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>A collection of employee order DTOs.</returns>
        /// <exception cref="EntityNotFoundException{Employee}">Thrown when the employee is not found.</exception>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when an order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<IEnumerable<ReturnEmployeeOrderDto>> Search(int employeeId);

        /// <summary>
        /// Retrieves an employee order by employee ID and order ID.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The employee order DTO.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<ReturnEmployeeOrderDto> Get(int employeeId, int orderId);

        /// <summary>
        /// Retrieves today's orders for an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>A collection of employee order DTOs.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when no orders are found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<IEnumerable<ReturnEmployeeOrderDto>> GetByEmpId(int employeeId);

        /// <summary>
        /// Retrieves all orders for an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>A collection of employee order DTOs.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when no orders are found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<IEnumerable<ReturnEmployeeOrderDto>> GetAllByEmpId(int employeeId);

        /// <summary>
        /// Accepts an order by assigning it to an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="orderID">The order ID.</param>
        /// <returns>The updated employee order DTO.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the order is already assigned to another employee.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<ReturnEmployeeOrderDto> AcceptOrder(int employeeId, int orderID);

        /// <summary>
        /// Delivers an order by an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The updated employee order DTO.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="InvalidOrderException">Thrown when the order is not ready for delivery.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<ReturnEmployeeOrderDto> DeliverOrder(int employeeId, int orderId);

        /// <summary>
        /// Picks up an order by an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The updated employee order DTO.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="InvalidOrderException">Thrown when the order is not ready for pickup.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public Task<ReturnEmployeeOrderDto> PickUpOrder(int employeeId, int orderId);
    }
}
