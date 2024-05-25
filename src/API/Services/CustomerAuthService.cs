using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class CustomerAuthService : ICustomerAuthService
    {
        private readonly ICustomerAuthRepository _customerAuthRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Customer> _tokenService;
        private readonly IMapper _mapper;

        public CustomerAuthService(
            ICustomerAuthRepository customerAuthRepository ,
            IRepository<int,Customer> customerRepository ,
            IPasswordHashService passwordHashServices,
            ITokenService<Customer> tokenService ,
            IMapper mapper)
        {
            _customerAuthRepository = customerAuthRepository;
            _customerRepository = customerRepository;
            _passwordHashService = passwordHashServices;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<ReturnCustomerLoginDto> Login(CustomerLoginDto customerLoginDto)
        {
            var customer = await _customerAuthRepository.Get(customerLoginDto.CustomerEmail);
            if (_passwordHashService.Verify(customerLoginDto.CustomerPassword, customer.CustomerAuth.Password))
            {
                var res = _mapper.Map<ReturnCustomerLoginDto>(customer);
                res.Token = _tokenService.GenerateToken(customer);
                return res;
            }
            throw new InvalidUserCredentialException();
        }

        public async Task<ReturnCustomerRegisterDto> Regiser(CustomerRegisterDto customerRegisterDto)
        {
            Customer customer = _mapper.Map<Customer>(customerRegisterDto);
            customer.CustomerAuth = new CustomerAuth
            {
                Password = _passwordHashService.Hash(customerRegisterDto.CustomerPassword)
            };
            var res = await _customerRepository.Add(customer);
            return _mapper.Map<ReturnCustomerRegisterDto>(res);
        }
    }
}
