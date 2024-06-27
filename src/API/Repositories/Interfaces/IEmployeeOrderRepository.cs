using API.Models;
using API.Models.Enums;

namespace API.Repositories.Interfaces
{
    public interface IEmployeeOrderRepository : IOrderRepository
    {
        public Task<IEnumerable<Order>> SearchOrderAsync(List<AddressCode> employeeRange);
        public Task<IEnumerable<Order>> GetTodayByEmployeeIdAsync(int employeeId);
        public Task<IEnumerable<Order>> GetAllByEmployeeIdAsync(int employeeId);
    }
}
