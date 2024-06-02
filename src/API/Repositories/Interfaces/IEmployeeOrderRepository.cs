using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Models.Enums;

namespace API.Repositories.Interfaces
{
    public interface IEmployeeOrderRepository
    {
        public Task<IEnumerable<Order>> SearchOrder(List<AddressCode> employeeRange);
        public Task<Order> Get(int orderId);
        public Task<IEnumerable<Order>> GetByEmpId(int EmployeeId);
        public Task<IEnumerable<Order>> GetAllByEmpId(int EmployeeId);
        public Task<Order> UpdateOrder(Order order);
    }
}