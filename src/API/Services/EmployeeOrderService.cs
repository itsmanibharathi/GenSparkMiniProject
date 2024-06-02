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
        private readonly IEmployeeOrderRepository _repository;
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeOrderService(IEmployeeOrderRepository repository, IRepository<int, Employee> employeeRepository, IMapper mapper)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ReturnEmployeeOrderDto>> Search(int employeeId)
        {
            try
            {
                var employeeRange = await GetCircularRange(employeeId);
                var res=await _repository.SearchOrder(employeeRange);
                return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res);
            }
            catch (EmployeeNotFoundException)
            {
                throw;
            }
            catch (OrderNotFoundException)
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

            var employee = await _employeeRepository.Get(employeeId);
            List<AddressCode> result = new List<AddressCode>();
            int totalAddresses = Enum.GetValues(typeof(AddressCode)).Length;
            int currentIndex = (int)employee.AddressCode;
            int range = 10;
            for (int i = -range; i <= range; i++)
            {
                int index = (currentIndex + i + totalAddresses) % totalAddresses;
                result.Add((AddressCode)index);
            }

            return result;
        }
        
        public async Task<ReturnEmployeeOrderDto> Get(int employeeId, int orderId)
        {
            var res = await _repository.Get(orderId);
            if (res.EmployeeId == employeeId)
            {
                return _mapper.Map<ReturnEmployeeOrderDto>(res);
            }
            throw new OrderNotFoundException();
        }

        public async Task<IEnumerable<ReturnEmployeeOrderDto>> GetByEmpId(int employeeId)
        {
            var res = await _repository.GetByEmpId(employeeId);
            return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
        }

        public async Task<IEnumerable<ReturnEmployeeOrderDto>> GetAllByEmpId(int employeeId)
        {
            var res = await _repository.GetAllByEmpId(employeeId);
            return _mapper.Map<IEnumerable<ReturnEmployeeOrderDto>>(res.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderStatus));
        }

        public async Task<ReturnEmployeeOrderDto> UpdateOrder(int employeeId, int orderId, OrderStatus orderStatus)
        {
            try
            {
                var order = await _repository.Get(orderId);
                if (order.EmployeeId != employeeId)
                {
                    throw new OrderNotFoundException();
                }
                order.OrderStatus = orderStatus;

                var res = await _repository.UpdateOrder(order);
                return _mapper.Map<ReturnEmployeeOrderDto>(res);
            }
            catch (OrderNotFoundException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new UnableToDoActionException("Unable to Preparing order", e);
            }
        }

        public async Task<ReturnEmployeeOrderDto> Accept(int employeeId, int orderID)
        {
            try
            {
                var order = await _repository.Get(orderID);
                if (order.EmployeeId != null)
                {
                    throw new OrderNotFoundException();
                }
                order.EmployeeId = employeeId;
                var res = await _repository.UpdateOrder(order);
                return _mapper.Map<ReturnEmployeeOrderDto>(res);
            }
            catch (OrderNotFoundException)
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
