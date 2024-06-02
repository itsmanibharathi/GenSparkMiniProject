using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Customer> _tokenService;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IPasswordHashService passwordHashServices,
            ITokenService<Customer> tokenService ,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _passwordHashService = passwordHashServices;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        public async Task<ReturnCustomerLoginDto> Login(CustomerLoginDto customerLoginDto)
        {
            var customer = await _customerRepository.GetByEmailId(customerLoginDto.CustomerEmail);
            if (_passwordHashService.Verify(customerLoginDto.CustomerPassword, customer.CustomerAuth.Password))
            {
                var res = _mapper.Map<ReturnCustomerLoginDto>(customer);
                res.Token = _tokenService.GenerateToken(customer);
                return res;
            }
            throw new InvalidUserCredentialException();
        }

        public async Task<ReturnCustomerRegisterDto> Register(CustomerRegisterDto customerRegisterDto)
        {
            Customer customer = _mapper.Map<Customer>(customerRegisterDto);
            customer.CustomerAuth = new CustomerAuth
            {
                Password = _passwordHashService.Hash(customerRegisterDto.CustomerPassword)
            };
            var res = await _customerRepository.AddAsync(customer);
            return _mapper.Map<ReturnCustomerRegisterDto>(res);
        }

        public async Task<ReturnCustomerDto> UpdatePhone(int customerId, string phone)
        {
            try
            {
                var customer = await _customerRepository.GetAsync(customerId);
                customer.CustomerPhone = phone;
                var res = await _customerRepository.UpdateAsync(customer);
                return _mapper.Map<ReturnCustomerDto>(res);
            }
            catch (EntityNotFoundException<Customer>)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ReturnCustomerDto> Get(int customerId)
        {
            try
            {
                var customer = await _customerRepository.GetAsync(customerId);
                return _mapper.Map<ReturnCustomerDto>(customer);
            }
            catch (EntityNotFoundException<Customer>)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
