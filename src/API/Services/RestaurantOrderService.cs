using API.Exceptions;
using API.Models;
using API.Models.DTOs.RestaurantDto;
using API.Models.Enums;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class RestaurantOrderService : IRestaurantOrderService
    {
        private readonly IRestaurantOrderRepository _restaurantOrderRepository;
        private readonly IMapper _mapper;

        public RestaurantOrderService(IRestaurantOrderRepository  restaurantOrderRepository, IMapper mapper)
        {
            _restaurantOrderRepository = restaurantOrderRepository;
            _mapper = mapper;
        }
        public async Task<ReturnRestaurantOrderDto> Get(int restaurantId, int orderId)
        {
            try
            {
                var res = await _restaurantOrderRepository.GetAsync(orderId);
                if (res.RestaurantId != restaurantId)
                {
                    throw new OrderNotFoundException(orderId);
                }
                return _mapper.Map<ReturnRestaurantOrderDto>(res);
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

        public async Task<IEnumerable<ReturnRestaurantOrderDto>> Get(int restaurantId)
        {
            try
            {
                var res = await _restaurantOrderRepository.GetToDayByRestaurantId(restaurantId);
                return _mapper.Map<IEnumerable<ReturnRestaurantOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
            }
            catch (EntityNotFoundException<Order>)
            {
                throw new Exception($"No orders found Today for restaurant {restaurantId}");
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to get orders for restaurant {restaurantId}", e);
            }
        }

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
        public async Task<ReturnRestaurantOrderDto> PreparingOrder(int restaurantId, int orderId)
        {
            try
            {
                var order = await _restaurantOrderRepository.GetAsync(orderId);
                if (order.RestaurantId != restaurantId)
                {
                    throw new UnauthorizedAccessException($"Order {orderId} does not belong to restaurant {restaurantId}");
                }
                if(order.OrderStatus != OrderStatus.Place)
                {
                    throw new EntityNotFoundException<Order>($"Order {orderId} not in place status");
                }
                order.OrderStatus = OrderStatus.Preparing;
                var res = await _restaurantOrderRepository.UpdateAsync(order);
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
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to Preparing order {orderId}", e);
            }
        }

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
                    throw new EntityNotFoundException<Order>($"Order {orderId} not in preparing status");
                }
                order.OrderStatus = OrderStatus.Prepared;
                var res = await _restaurantOrderRepository.UpdateAsync(order);
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
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to Prepared order {orderId}", e);
            }
        }
    }
}
