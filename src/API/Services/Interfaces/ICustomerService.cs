using API.Models.DTOs.CustomerDto;

namespace API.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<ReturnCustomerRegisterDto> Register(CustomerRegisterDto customerRegisterDto);
        public Task<ReturnCustomerLoginDto> Login(CustomerLoginDto customerLoginDto);
        public Task<ReturnCustomerDto> Get(int customerId);
        public Task<ReturnCustomerDto> UpdatePhone(int customerId, string phone);
    }
}
