using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Models.Enums;

namespace API.Services.Interfaces
{
    public interface IEmployeeOrderService
    {
        public Task<IEnumerable<ReturnEmployeeOrderDto>> Search(int employeeId);
        public Task<ReturnEmployeeOrderDto> Accept(int employeeId,int orderID);
        public Task<ReturnEmployeeOrderDto> Get(int employeeId, int orderId);
        public Task<IEnumerable<ReturnEmployeeOrderDto>> GetByEmpId(int employeeId);
        public Task<IEnumerable<ReturnEmployeeOrderDto>> GetAllByEmpId(int employeeId);
        public Task<ReturnEmployeeOrderDto> UpdateOrder(int employeeId, int orderId, OrderStatus orderStatus);
    }
}