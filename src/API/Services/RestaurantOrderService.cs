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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ReturnRestaurantOrderDto>> Get(int restaurantId)
        {
            var res = await _restaurantOrderRepository.GetToDayByRestaurantId(restaurantId);
            return _mapper.Map<IEnumerable<ReturnRestaurantOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
        }

        public async Task<IEnumerable<ReturnRestaurantOrderDto>> GetAll(int restaurantId)
        {
            var res = await _restaurantOrderRepository.GetAllByRestaurantId(restaurantId);
            return _mapper.Map<IEnumerable<ReturnRestaurantOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
        }
        public async Task<ReturnRestaurantOrderDto> PreparingOrder(int restaurantId, int orderId)
        {
            try
            {
                var order = await _restaurantOrderRepository.GetAsync(orderId);
                if (order.RestaurantId == restaurantId && order.OrderStatus == OrderStatus.Place)
                {
                    order.OrderStatus = OrderStatus.Preparing;
                    var res = await _restaurantOrderRepository.UpdateAsync(order);
                    return _mapper.Map<ReturnRestaurantOrderDto>(res);
                }
                else
                {
                    throw new EntityNotFoundException<Order>(orderId);
                }
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
                if (order.RestaurantId == restaurantId && order.OrderStatus == OrderStatus.Preparing)
                {
                    order.OrderStatus = OrderStatus.Prepared;
                    var res = await _restaurantOrderRepository.UpdateAsync(order);
                    return _mapper.Map<ReturnRestaurantOrderDto>(res);
                }
                else
                {
                    throw new EntityNotFoundException<Order>(orderId);
                }
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException($"Unable to Pepared order {orderId}", e);
            }
        }
    }
}
