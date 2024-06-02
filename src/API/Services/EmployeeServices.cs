using API.Exceptions;
using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Employee> _tokenService;
        private readonly IMapper _mapper;

        public EmployeeService(
            IEmployeeRepository employeeRepository,
            IPasswordHashService passwordHashServices,
            ITokenService<Employee> tokenService,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _passwordHashService = passwordHashServices;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<ReturnEmployeeLoginDto> Login(EmployeeLoginDto employeeLoginDto)
        {
            try
            {
                var employee = await _employeeRepository.GetByEmailIdAsync(employeeLoginDto.EmployeeEmail);
                if (_passwordHashService.Verify(employeeLoginDto.Password, employee.EmployeeAuth.Password))
                {
                    var res = _mapper.Map<ReturnEmployeeLoginDto>(employee);
                    res.Token = _tokenService.GenerateToken(employee);
                    return res;
                }
                throw new InvalidUserCredentialException();
            }
            catch (EntityNotFoundException<Employee>)
            {
                throw;
            }
            catch (InvalidUserCredentialException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to login", ex);
            }
        }

        public async Task<ReturnEmployeeRegisterDto> Register(EmployeeRegisterDto employeeRegisterDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeRegisterDto);
            employee.EmployeeAuth = new EmployeeAuth
            {
                Password = _passwordHashService.Hash(employeeRegisterDto.Password)
            };
            var res = await _employeeRepository.AddAsync(employee);
            return _mapper.Map<ReturnEmployeeRegisterDto>(res);

        }
    }
}
