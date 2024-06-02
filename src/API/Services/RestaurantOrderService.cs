//using API.Exceptions;
//using API.Models.DTOs.RestaurantDto;
//using API.Models.Enums;
//using API.Repositories.Interfaces;
//using API.Services.Interfaces;
//using AutoMapper;

//namespace API.Services
//{
//    public class RestaurantOrderService : IRestaurantOrderService
//    {
//        private readonly IRestaurantOrderRepository _repository;
//        private readonly IMapper _mapper;

//        public RestaurantOrderService(IRestaurantOrderRepository repository, IMapper mapper ) 
//        {
//            _repository = repository;
//            _mapper = mapper;
//        }
//        public async Task<ReturnRestaurantOrderDto> Get(int restaurantId, int orderId)
//        {
//            var res= await _repository.Get(restaurantId, orderId);
//            return _mapper.Map<ReturnRestaurantOrderDto>(res);
//        }

//        public async Task<IEnumerable<ReturnRestaurantOrderDto>> Get(int restaurantId)
//        {
//            var res = await _repository.Get(restaurantId);
//            return _mapper.Map<IEnumerable<ReturnRestaurantOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
//        }

//        public async Task<IEnumerable<ReturnRestaurantOrderDto>> GetAll(int restaurantId)
//        {
//            var res = await _repository.GetAll(restaurantId);
//            return _mapper.Map<IEnumerable<ReturnRestaurantOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
//        }
//        public async Task<ReturnRestaurantOrderDto> PreparingOrder(int restaurantId, int orderId)
//        {
//            try
//            {
//                var order = await _repository.Get(restaurantId, orderId);
//                order.OrderStatus = OrderStatus.Preparing;
//                var res= await _repository.Update(order);
//                return _mapper.Map<ReturnRestaurantOrderDto>(res);
//            }
//            catch (OrderNotFoundException)
//            {
//                throw;
//            }
//            catch (Exception e)
//            {
//                throw new UnableToDoActionException("Unable to Preparing order", e);
//            }
//        }

//        public async Task<ReturnRestaurantOrderDto> PreparedOrder(int restaurantId, int orderId)
//        {
//            try
//            {
//                var order = await _repository.Get(restaurantId, orderId);
//                order.OrderStatus = OrderStatus.Prepared;
//                var res = await _repository.Update(order);
//                return _mapper.Map<ReturnRestaurantOrderDto>(res);
//            }
//            catch (OrderNotFoundException)
//            {
//                throw;
//            }
//            catch (Exception e)
//            {
//                throw new UnableToDoActionException("Unable to Preparing order", e);
//            }
//        }
//    }
//}
