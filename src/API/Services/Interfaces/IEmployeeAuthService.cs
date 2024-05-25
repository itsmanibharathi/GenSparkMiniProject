using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface IEmployeeAuthService
    {
        public Task<ReturnEmployeeLoginDto> Login(EmployeeLoginDto employeeLoginDto);
        public Task<ReturnCustomerRegisterDto> Register(EmployeeRegisterDto employeeRegisterDto);

    }
}
