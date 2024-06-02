using API.Models;
using API.Models.Enums;

namespace API.Repositories.Interfaces
{
    public interface IEmployeeOrderRepository : IOrderRepository
    {
        public Task<IEnumerable<Order>> SearchOrderAsync(List<AddressCode> employeeRange);
        public Task<IEnumerable<Order>> GetTodayByEmployeeIdAsunc(int employeeId);
        public Task<IEnumerable<Order>> GetAllByEmployeeIdAsunc(int employeeId);
    }
}
