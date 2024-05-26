using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface IEmployeeAuthService
    {
        public Task<ReturnEmployeeLoginDto> Login(EmployeeLoginDto employeeLoginDto);
        public Task<ReturnEmployeeRegisterDto> Register(EmployeeRegisterDto employeeRegisterDto);

    }
}
