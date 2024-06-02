//using API.Exceptions;
//using API.Models;
//using API.Models.DTOs.EmployeeDto;
//using API.Models.Enums;
//using API.Repositories.Interfaces;
//using API.Services.Interfaces;
//using AutoMapper;
//using System.Net;

//namespace API.Services
//{
//    public class EmployeeOrderService : IEmployeeOrderService
//    {
//        private readonly IEmployeeOrderRepository _repository;
//        private readonly IRepository1<int, Employee> _employeeRepository;
//        private readonly IMapper _mapper;

//        public EmployeeOrderService(IEmployeeOrderRepository repository, IRepository1<int, Employee> employeeRepository, IMapper mapper)
//        {
//            _repository = repository;
//            _employeeRepository = employeeRepository;
//            _mapper = mapper;
//        }


//        public async Task<IEnumerable<ReturnEmployeeOrderDto>> Search(int employeeId)
//        {
//            try
//            {
//                var employeeRange = await GetCircularRange(employeeId);
//                var res=await _repository.SearchOrder(employeeRange);
//                return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res);
//            }
//            catch (EmployeeNotFoundException)
//            {
//                throw;
//            }
//            catch (OrderNotFoundException)
//            {
//                throw;
//            }
//            catch (Exception e)
//            {
//                throw new UnableToDoActionException("Unable to search orders", e);
//            }
//        }
//        public async Task<List<AddressCode>> GetCircularRange(int employeeId)
//        {

//            var employee = await _employeeRepository.Get(employeeId);
//            List<AddressCode> result = new List<AddressCode>();
//            int totalAddresses = Enum.GetValues(typeof(AddressCode)).Length;
//            int currentIndex = (int)employee.AddressCode;
//            int range = 10;
//            for (int i = -range; i <= range; i++)
//            {
//                int index = (currentIndex + i + totalAddresses) % totalAddresses;
//                result.Add((AddressCode)index);
//            }

//            return result;
//        }
        
//        public async Task<ReturnEmployeeOrderDto> Get(int employeeId, int orderId)
//        {
//            var res = await _repository.Get(orderId);
//            if (res.EmployeeId == employeeId)
//            {
//                return _mapper.Map<ReturnEmployeeOrderDto>(res);
//            }
//            throw new OrderNotFoundException();
//        }

//        public async Task<IEnumerable<ReturnEmployeeOrderDto>> GetByEmpId(int employeeId)
//        {
//            var res = await _repository.GetByEmpId(employeeId);
//            return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
//        }

//        public async Task<IEnumerable<ReturnEmployeeOrderDto>> GetAllByEmpId(int employeeId)
//        {
//            var res = await _repository.GetAllByEmpId(employeeId);
//            return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
//        }

        
//        public async Task<ReturnEmployeeOrderDto> AcceptOrder(int employeeId, int orderID)
//        {
//            try
//            {
//                var order = await _repository.Get(orderID);
//                if (order.EmployeeId != null)
//                {
//                    throw new OrderNotFoundException();
//                }
//                order.EmployeeId = employeeId;
//                var res = await _repository.UpdateOrder(order);
//                return _mapper.Map<ReturnEmployeeOrderDto>(res);
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

//        public async Task<ReturnEmployeeOrderDto> DeliverOrder(int employeeId, int orderId, decimal? amount)
//        {
//            try
//            {
//                var order = await _repository.Get(orderId);
//                if (order.EmployeeId != employeeId)
//                {
//                    throw new OrderNotFoundException();
//                }
//                if (order.PaymentMethod == PaymentMethod.COD)
//                {
//                    if (amount != null && amount != order.TotalAmount)
//                    {
//                        throw new UnableToDoActionException("Amount is not correct");
//                    }
//                    order.Employee.Balance += amount ?? 0;
//                }
//                order.OrderStatus = OrderStatus.Delivered;
//                order.DeliveryDate = DateTime.Now;

//                var res = await _repository.UpdateOrder(order);
//                return _mapper.Map<ReturnEmployeeOrderDto>(res);
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
//        public async Task<ReturnEmployeeOrderDto> PicUpOrder(int employeeId, int orderId)
//        {
//            try
//            {
//                var order = await _repository.Get(orderId);
//                if (order.EmployeeId != employeeId)
//                {
//                    throw new OrderNotFoundException();
//                }
//                order.OrderStatus = OrderStatus.PickedUp;

//                var res = await _repository.UpdateOrder(order);
//                return _mapper.Map<ReturnEmployeeOrderDto>(res);
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
