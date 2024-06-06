using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Provides services for managing customers, including login, registration, updating phone, and retrieving customer information.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly ITokenService<Customer> _tokenService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="customerRepository">The repository for accessing customer data.</param>
        /// <param name="passwordHashServices">The service for hashing and verifying passwords.</param>
        /// <param name="tokenService">The service for generating tokens.</param>
        /// <param name="mapper">The mapper for mapping between models and DTOs.</param>
        public CustomerService(
            ICustomerRepository customerRepository,
            IPasswordHashService passwordHashServices,
            ITokenService<Customer> tokenService,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _passwordHashService = passwordHashServices;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        /// <summary>
        /// Authenticates a customer and generates a token.
        /// </summary>
        /// <param name="customerLoginDto">The DTO containing the customer's login credentials.</param>
        /// <returns>The DTO containing the customer's details and token.</returns>
        /// <exception cref="InvalidUserCredentialException">Thrown when the customer's credentials are invalid.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs during login.</exception>
        public async Task<ReturnCustomerLoginDto> Login(CustomerLoginDto customerLoginDto)
        {
            try
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
            catch (EntityNotFoundException<Customer>)
            {
                throw new InvalidUserCredentialException();
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
        /// Registers a new customer.
        /// </summary>
        /// <param name="customerRegisterDto">The DTO containing the customer's registration details.</param>
        /// <returns>The DTO containing the registered customer's details.</returns>
        /// <exception cref="EntityAlreadyExistsException{Customer}">Thrown when the customer already exists.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs during registration.</exception>
        public async Task<ReturnCustomerRegisterDto> Register(CustomerRegisterDto customerRegisterDto)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(customerRegisterDto);
                customer.CustomerAuth = new CustomerAuth
                {
                    Password = _passwordHashService.Hash(customerRegisterDto.CustomerPassword)
                };
                var res = await _customerRepository.AddAsync(customer);
                return _mapper.Map<ReturnCustomerRegisterDto>(res);
            }
            catch (EntityAlreadyExistsException<Customer>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to register", ex);
            }
        }

        /// <summary>
        /// Updates the phone number of an existing customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="phone">The new phone number of the customer.</param>
        /// <returns>The DTO containing the updated customer's details.</returns>
        /// <exception cref="EntityNotFoundException{Customer}">Thrown when the customer is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the update.</exception>
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

        /// <summary>
        /// Retrieves the details of a customer by ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>The DTO containing the customer's details.</returns>
        /// <exception cref="EntityNotFoundException{Customer}">Thrown when the customer is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during retrieval.</exception>
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
