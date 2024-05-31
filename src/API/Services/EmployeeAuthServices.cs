using API.Exceptions;
using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class EmployeeAuthService : IEmployeeAuthService
    {
        private readonly IEmployeeAuthRepository _employeeAuthRepository;
        private readonly IRepository<int, Employee> _employeeRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Employee> _tokenService;
        private readonly IMapper _mapper;

        public EmployeeAuthService(
            IEmployeeAuthRepository employeeAuthRepository,
            IRepository<int, Employee> employeeRepository,
            IPasswordHashService passwordHashServices,
            ITokenService<Employee> tokenService,
            IMapper mapper)
        {
            _employeeAuthRepository = employeeAuthRepository;
            _employeeRepository = employeeRepository;
            _passwordHashService = passwordHashServices;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<ReturnEmployeeLoginDto> Login(EmployeeLoginDto employeeLoginDto)
        {
            var employee = await _employeeAuthRepository.Get(employeeLoginDto.EmployeeEmail);
            if (_passwordHashService.Verify(employeeLoginDto.Password, employee.EmployeeAuth.Password))
            {
                var res = _mapper.Map<ReturnEmployeeLoginDto>(employee);
                res.Token = _tokenService.GenerateToken(employee);
                return res;
            }
            throw new InvalidUserCredentialException();
        }

        public Task<ReturnEmployeeRegisterDto> Register(EmployeeRegisterDto employeeRegisterDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeRegisterDto);
            employee.EmployeeAuth = new EmployeeAuth
            {
                Password = _passwordHashService.Hash(employeeRegisterDto.Password)
            };
            return _employeeRepository.Add(employee).ContinueWith(e => _mapper.Map<ReturnEmployeeRegisterDto>(e.Result));
        }
    }
}
