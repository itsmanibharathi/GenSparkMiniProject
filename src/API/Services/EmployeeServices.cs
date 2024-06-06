using API.Exceptions;
using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Service for managing employee operations.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Employee> _tokenService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="passwordHashServices">The password hash service.</param>
        /// <param name="tokenService">The token service.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
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

        /// <summary>
        /// Authenticates an employee based on login credentials.
        /// </summary>
        /// <param name="employeeLoginDto">The employee login DTO containing email and password.</param>
        /// <returns>The logged-in employee DTO with a token.</returns>
        /// <exception cref="EntityNotFoundException{Employee}">Thrown when the employee is not found.</exception>
        /// <exception cref="InvalidUserCredentialException">Thrown when the credentials are invalid.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
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

        /// <summary>
        /// Registers a new employee.
        /// </summary>
        /// <param name="employeeRegisterDto">The employee register DTO containing employee details.</param>
        /// <returns>The registered employee DTO.</returns>
        /// <exception cref="EntityAlreadyExistsException{Employee}">Thrown when the employee already exists.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnEmployeeRegisterDto> Register(EmployeeRegisterDto employeeRegisterDto)
        {
            try
            {
                Employee employee = _mapper.Map<Employee>(employeeRegisterDto);
                employee.EmployeeAuth = new EmployeeAuth
                {
                    Password = _passwordHashService.Hash(employeeRegisterDto.Password)
                };
                var res = await _employeeRepository.AddAsync(employee);
                return _mapper.Map<ReturnEmployeeRegisterDto>(res);
            }
            catch (EntityAlreadyExistsException<Employee>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to register", ex);
            }
        }
    }
}
