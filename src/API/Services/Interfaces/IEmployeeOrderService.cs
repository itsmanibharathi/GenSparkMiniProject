using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Models.Enums;

namespace API.Services.Interfaces
{
    public interface IEmployeeOrderService
    {
        public Task<IEnumerable<ReturnEmployeeOrderDto>> Search(int employeeId);
        public Task<ReturnEmployeeOrderDto> Get(int employeeId, int orderId);
        public Task<IEnumerable<ReturnEmployeeOrderDto>> GetByEmpId(int employeeId);
        public Task<IEnumerable<ReturnEmployeeOrderDto>> GetAllByEmpId(int employeeId);
        public Task<ReturnEmployeeOrderDto> DeliverOrder(int employeeId, int orderId, decimal? amount);
        public Task<ReturnEmployeeOrderDto> AcceptOrder(int employeeId,int orderID);
        public Task<ReturnEmployeeOrderDto> PicUpOrder(int employeeId, int orderID);
    }
}