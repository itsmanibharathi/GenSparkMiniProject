using API.Exceptions;
using API.Models;
using API.Models.DTOs.RestaurantDto;
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
    /// Service for managing restaurant order operations.
    /// </summary>
    public class RestaurantOrderService : IRestaurantOrderService
    {
        private readonly IRestaurantOrderRepository _restaurantOrderRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantOrderService"/> class.
        /// </summary>
        /// <param name="restaurantOrderRepository">The restaurant order repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public RestaurantOrderService(IRestaurantOrderRepository restaurantOrderRepository, IMapper mapper)
        {
            _restaurantOrderRepository = restaurantOrderRepository;
            _mapper = mapper;
        }

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
        public async Task<ReturnRestaurantOrderDto> Get(int restaurantId, int orderId)
        {
            try
            {
                var res = await _restaurantOrderRepository.GetAsync(orderId);
                if (res.RestaurantId != restaurantId)
                {
                    throw new UnauthorizedAccessException($"Order {orderId} does not belong to restaurant {restaurantId}");
                }
                return _mapper.Map<ReturnRestaurantOrderDto>(res);
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to get order {orderId}", e);
            }
        }

        /// <summary>
        /// Retrieves all today's orders for a restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <returns>A collection of today's orders for the restaurant.</returns>
        /// <exception cref="EntityNotFoundException{Orde}> Thrown when no orders are found today for the restaurant. </exception>
       
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs while retrieving the orders.</exception>
        public async Task<IEnumerable<ReturnRestaurantOrderDto>> Get(int restaurantId)
        {
            try
            {
                var res = await _restaurantOrderRepository.GetToDayByRestaurantId(restaurantId);
                return _mapper.Map<IEnumerable<ReturnRestaurantOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
            }
            catch (EntityNotFoundException<Order>)
            {
                throw new EntityNotFoundException<Order>($"| No orders found today for restaurant {restaurantId}");
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to get orders for restaurant {restaurantId}", e);
            }
        }

        /// <summary>
        /// Retrieves all orders for a restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant.</param>
        /// <returns>A collection of all orders for the restaurant.</returns>
        /// <exception cref="Exception">Thrown when no orders are found for the restaurant.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs while retrieving the orders.</exception>
        public async Task<IEnumerable<ReturnRestaurantOrderDto>> GetAll(int restaurantId)
        {
            try
            {
                var res = await _restaurantOrderRepository.GetAllByRestaurantId(restaurantId);
                return _mapper.Map<IEnumerable<ReturnRestaurantOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
            }
            catch (EntityNotFoundException<Order>)
            {
                throw new Exception($"No orders found for restaurant {restaurantId}");
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to get orders for restaurant {restaurantId}", e);
            }
        }

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
        public async Task<ReturnRestaurantOrderDto> PreparingOrder(int restaurantId, int orderId)
        {
            try
            {
                var order = await _restaurantOrderRepository.GetAsync(orderId);
                if (order.RestaurantId != restaurantId)
                {
                    throw new UnauthorizedAccessException($"Order {orderId} does not belong to restaurant {restaurantId}");
                }
                if (order.OrderStatus != OrderStatus.Place)
                {
                    throw new InvalidOrderException($"Order {orderId} not in place status");
                }
                order.OrderStatus = OrderStatus.Preparing;
                var res = await _restaurantOrderRepository.UpdateAsync(order);
                return _mapper.Map<ReturnRestaurantOrderDto>(res);
            }
            catch (InvalidOrderException)
            {
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to Preparing order {orderId}", e);
            }
        }

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
        public async Task<ReturnRestaurantOrderDto> PreparedOrder(int restaurantId, int orderId)
        {
            try
            {
                var order = await _restaurantOrderRepository.GetAsync(orderId);
                if (order.RestaurantId != restaurantId)
                {
                    throw new UnauthorizedAccessException($"Order {orderId} does not belong to restaurant {restaurantId}");
                }
                if (order.OrderStatus != OrderStatus.Preparing)
                {
                    throw new InvalidOrderException($"Order {orderId} not in preparing status");
                }
                order.OrderStatus = OrderStatus.Prepared;
                var res = await _restaurantOrderRepository.UpdateAsync(order);
                return _mapper.Map<ReturnRestaurantOrderDto>(res);
            }
            catch (InvalidOrderException)
            {
                throw;
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to Prepared order {orderId}", e);
            }
        }
    }
}

