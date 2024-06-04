using API.Exceptions;
using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Models.Enums;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System.Net;

namespace API.Services
{
    public class EmployeeOrderService : IEmployeeOrderService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeOrderRepository _employeeOrderRepository;
        private readonly IMapper _mapper;

        public EmployeeOrderService(
            IEmployeeRepository employeeRepository,
            IEmployeeOrderRepository employeeOrderRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _employeeOrderRepository = employeeOrderRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ReturnEmployeeOrderDto>> Search(int employeeId)
        {
            try
            {
                var employeeRange = await GetCircularRange(employeeId);
                var res = await _employeeOrderRepository.SearchOrderAsync(employeeRange);
                return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res);
            }
            catch ( EntityNotFoundException<Employee> )
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
        public async Task<List<AddressCode>> GetCircularRange(int employeeId)
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

        public async Task<ReturnEmployeeOrderDto> Get(int employeeId, int orderId)
        {
            var res = await _employeeOrderRepository.GetAsync(orderId);
            if (res.EmployeeId == employeeId)
            {
                return _mapper.Map<ReturnEmployeeOrderDto>(res);
            }
            throw new EntityNotFoundException<Order>(orderId);
        }

        public async Task<IEnumerable<ReturnEmployeeOrderDto>> GetByEmpId(int employeeId)
        {
            var res = await _employeeOrderRepository.GetTodayByEmployeeIdAsunc(employeeId);
            return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
        }

        public async Task<IEnumerable<ReturnEmployeeOrderDto>> GetAllByEmpId(int employeeId)
        {
            var res = await _employeeOrderRepository.GetAllByEmployeeIdAsunc(employeeId);
            return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
        }


        public async Task<ReturnEmployeeOrderDto> AcceptOrder(int employeeId, int orderID)
        {
            try
            {
                var order = await _employeeOrderRepository.GetAsync(orderID);
                if (order.EmployeeId != null )
                {
                    throw new EntityNotFoundException<Order>(orderID);
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
                throw;
            }
        }

        public async Task<ReturnEmployeeOrderDto> DeliverOrder(int employeeId, int orderId)
        {
            try
            {
                var order = await _employeeOrderRepository.GetAsync(orderId);
                if (order.EmployeeId == employeeId && order.OrderStatus  == OrderStatus.PickedUp )
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
                throw new EntityNotFoundException<Order>(orderId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReturnEmployeeOrderDto> PicUpOrder(int employeeId, int orderId)
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
                throw new UnableToDoActionException("Unable to Preparing order", e);
            }
        }
    }
}
