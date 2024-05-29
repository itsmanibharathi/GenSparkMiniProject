using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IEmployeeAuthRepository
    {
        public Task<Employee> Get(string email);
    }
}
