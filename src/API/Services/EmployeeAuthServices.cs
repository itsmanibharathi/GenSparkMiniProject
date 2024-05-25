using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class EmployeeAuthService : IEmployeeAuthService
    {
        private readonly IEmployeeAuthRepository _EmployeeAuthRepository;
        private readonly IRepository<int, Employee> _EmployeeRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Employee> _tokenService;
        private readonly IMapper _mapper;

        public EmployeeAuthService(
            IEmployeeAuthRepository EmployeeAuthRepository,
            IRepository<int, Employee> EmployeeRepository,
            IPasswordHashService passwordHashServices,
            ITokenService<Employee> tokenService,
            IMapper mapper)
        {
            _EmployeeAuthRepository = EmployeeAuthRepository;
            _EmployeeRepository = EmployeeRepository;
            _passwordHashService = passwordHashServices;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<ReturnEmployeeLoginDto> Login(EmployeeLoginDto employeeLoginDto)
        {
            var employee = await _EmployeeAuthRepository.Get(employeeLoginDto.EmployeeEmail);
            if (_passwordHashService.Verify(employeeLoginDto.Password, employee.EmployeeAuth.Password))
            {
                var res = _mapper.Map<ReturnEmployeeLoginDto>(employee);
                res.Token = _tokenService.GenerateToken(employee);
                return res;
            }
            throw new InvalidUserCredentialException();
        }

        public async Task<ReturnCustomerRegisterDto> Register(EmployeeRegisterDto employeeRegisterDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeRegisterDto);
            employee.EmployeeAuth = new EmployeeAuth
            {
                Password = _passwordHashService.Hash(employeeRegisterDto.Password)
            };
            var res = await _EmployeeRepository.Add(employee);
            return _mapper.Map<ReturnCustomerRegisterDto>(res);
        }
    }
}
