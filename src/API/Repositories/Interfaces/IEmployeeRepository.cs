using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IEmployeeRepository : IRepository<int,Employee>
    {
        public Task<Employee> GetByEmailIdAsync(string emailId);
    }
}
