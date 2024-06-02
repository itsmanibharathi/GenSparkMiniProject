using API.Models.DTOs.EmployeeDto;

namespace API.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<ReturnEmployeeLoginDto> Login(EmployeeLoginDto employeeLoginDto);
        public Task<ReturnEmployeeRegisterDto> Register(EmployeeRegisterDto employeeRegisterDto);

    }
}
