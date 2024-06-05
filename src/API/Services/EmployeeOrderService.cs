using API.Exceptions;
using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Models.Enums;
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
    /// Service class for managing employee orders.
    /// </summary>
    public class EmployeeOrderService : IEmployeeOrderService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeOrderRepository _employeeOrderRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeOrderService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="employeeOrderRepository">The employee order repository.</param>
        /// <param name="mapper">The mapper instance.</param>
        public EmployeeOrderService(
            IEmployeeRepository employeeRepository,
            IEmployeeOrderRepository employeeOrderRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeOrderRepository = employeeOrderRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Searches for orders within the circular range of addresses assigned to the employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>A collection of employee order DTOs.</returns>
        /// <exception cref="EntityNotFoundException{Employee}">Thrown when the employee is not found.</exception>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when an order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnEmployeeOrderDto>> Search(int employeeId)
        {
            try
            {
                var employeeRange = await GetCircularRange(employeeId);
                var res = await _employeeOrderRepository.SearchOrderAsync(employeeRange);
                return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res);
            }
            catch (EntityNotFoundException<Employee>)
            {
                throw;
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to search orders", e);
            }
        }

        /// <summary>
        /// Gets the circular range of addresses assigned to the employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>A list of address codes.</returns>
        /// <exception cref="EntityNotFoundException{Employee}">Thrown when the employee is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        async Task<List<AddressCode>> GetCircularRange(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetAsync(employeeId);
                List<AddressCode> result = new List<AddressCode>();
                int totalAddresses = Enum.GetValues(typeof(AddressCode)).Length;
                int currentIndex = (int)employee.AddressCode;
                int range = 5;
                for (int i = -range; i <= range; i++)
                {
                    int index = (currentIndex + i + totalAddresses) % totalAddresses;
                    result.Add((AddressCode)index);
                }
                return result;
            }
            catch (EntityNotFoundException<Employee>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to get circular range", e);
            }
        }

        /// <summary>
        /// Retrieves an employee order by ID.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The employee order DTO.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnEmployeeOrderDto> Get(int employeeId, int orderId)
        {
            try
            {
                var res = await _employeeOrderRepository.GetAsync(orderId);
                if (res.EmployeeId == employeeId)
                {
                    return _mapper.Map<ReturnEmployeeOrderDto>(res);
                }
                throw new EntityNotFoundException<Order>($"Order {orderId} not found for employee {employeeId}");
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to get order {orderId}", e);
            }
        }

        /// <summary>
        /// Retrieves today's orders for an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>A collection of employee order DTOs.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when no orders are found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnEmployeeOrderDto>> GetByEmpId(int employeeId)
        {
            try
            {
                var res = await _employeeOrderRepository.GetTodayByEmployeeIdAsunc(employeeId);
                return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
            }
            catch (EntityNotFoundException<Order>)
            {
                throw new EntityNotFoundException<Order>($"No orders found for employee {employeeId}");
            }
            catch (UnableToDoActionException)
            {
                throw new UnableToDoActionException($"Unable to get orders for employee {employeeId}");
            }
        }

        /// <summary>
        /// Retrieves all orders for an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>A collection of employee order DTOs.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when no orders are found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnEmployeeOrderDto>> GetAllByEmpId(int employeeId)
        {
            try
            {
                var res = await _employeeOrderRepository.GetAllByEmployeeIdAsunc(employeeId);
                return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
            }
            catch (EntityNotFoundException<Order>)
            {
                throw new EntityNotFoundException<Order>($"No orders found for employee {employeeId}");
            }
            catch (UnableToDoActionException)
            {
                throw new UnableToDoActionException($"Unable to get orders for employee {employeeId}");
            }
        }

        /// <summary>
        /// Assigns an order to an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="orderID">The order ID.</param>
        /// <returns>The updated employee order DTO.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the order is already assigned to another employee.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnEmployeeOrderDto> AcceptOrder(int employeeId, int orderID)
        {
            try
            {
                var order = await _employeeOrderRepository.GetAsync(orderID);
                if (order.EmployeeId != null)
                {
                    throw new UnauthorizedAccessException($"Order {orderID} is already assigned to another employee");
                }
                order.EmployeeId = employeeId;
                var res = await _employeeOrderRepository.UpdateAsync(order);
                return _mapper.Map<ReturnEmployeeOrderDto>(res);
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception)
            {
                throw new UnableToDoActionException($"Unable to accept order {orderID}");
            }
        }

        /// <summary>
        /// Delivers an order by an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The updated employee order DTO.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="InvalidOrderException">Thrown when the order is not ready for delivery.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnEmployeeOrderDto> DeliverOrder(int employeeId, int orderId)
        {
            try
            {
                var order = await _employeeOrderRepository.GetAsync(orderId);
                if (order.EmployeeId == employeeId && order.OrderStatus == OrderStatus.PickedUp)
                {
                    if (order.PaymentMethod == PaymentMethod.COD)
                    {
                        order.Employee.Balance += order.TotalAmount;
                        order.CashPayment.ReceiveBy = employeeId;
                        order.CashPayment.PaymentStatus = PaymentStatus.Paid;
                        order.CashPayment.PaymentDate = DateTime.Now;
                    }
                    order.OrderStatus = OrderStatus.Delivered;
                    order.DeliveryDate = DateTime.Now;
                    var res = await _employeeOrderRepository.UpdateAsync(order);
                    return _mapper.Map<ReturnEmployeeOrderDto>(res);
                }
                throw new InvalidOrderException($"Order {orderId} is not ready for delivery");
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to deliver order", e);
            }
        }

        /// <summary>
        /// Picks up an order by an employee.
        /// </summary>
        /// <param name="employeeId">The employee ID.</param>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The updated employee order DTO.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="InvalidOrderException">Thrown when the order is not ready for pickup.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnEmployeeOrderDto> PickUpOrder(int employeeId, int orderId)
        {
            try
            {
                var order = await _employeeOrderRepository.GetAsync(orderId);
                if (order.EmployeeId == employeeId && order.OrderStatus == OrderStatus.Prepared)
                {
                    order.OrderStatus = OrderStatus.PickedUp;
                    var res = await _employeeOrderRepository.UpdateAsync(order);
                    return _mapper.Map<ReturnEmployeeOrderDto>(res);
                }
                throw new InvalidOrderException($"Order {orderId} is not ready for pickup");
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to pick up order", e);
            }
        }
    }
}



