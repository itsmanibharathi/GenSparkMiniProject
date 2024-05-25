using API.Models;
using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface ICustomerAuthService
    {
        public Task<ReturnCustomerRegisterDto> Register(CustomerRegisterDto customerRegisterDto);
        public Task<ReturnCustomerLoginDto> Login(CustomerLoginDto customerLoginDto);
    }
}
