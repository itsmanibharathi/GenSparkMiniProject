using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Models.Enums;
using API.Services;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Services.CustomerServices
{
    [TestFixture]
    internal class CustomerServiceTest : ServicesTestBase
    {
        private ICustomerService _customerService;

        [SetUp]
        public void Setup()
        {
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);
        }

        [Test]
        public async Task RegisterCustomer()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };

            // Act
            var result = await _customerService.Register(customer);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CustomerId,1);
        }

        [Test]
        public async Task RegisterCustomerDuplicateException()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };

            // Act
            var result = await _customerService.Register(customer);

            // Assert
            Assert.ThrowsAsync<EntityAlreadyExistsException<Customer>>(async () => await _customerService.Register(customer));
        }

        [Test]
        public async Task RegisterCustomerIntenalServrError()
        {
            // Arrange
            DummyDB();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);

            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };

            // Assert
            Assert.ThrowsAsync<UnableToDoActionException>(async () => await _customerService.Register(customer));
        }




        [Test]
        public async Task LoginCustomer()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };
            var result = await _customerService.Register(customer);


            // Act
            var login = new CustomerLoginDto()
            {
                CustomerEmail = "kiko@gmail.com",
                CustomerPassword = "string123"
            };
            var loginResult = await _customerService.Login(login);
            Assert.IsNotNull(loginResult);
            Assert.IsNotNull(loginResult.Token);
        }

        [Test]
        public async Task LoginCustomerError1()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };
            var result = await _customerService.Register(customer);


            // Act
            var login = new CustomerLoginDto()
            {
                CustomerEmail = "kiko@gmail.in",
                CustomerPassword = "string123"
            };
            Assert.ThrowsAsync<InvalidUserCredentialException>(async () => await _customerService.Login(login));
        }

        [Test]
        public async Task LoginCustomerError2()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };
            var result = await _customerService.Register(customer);


            // Act
            var login = new CustomerLoginDto()
            {
                CustomerEmail = "kiko@gmail.com",
                CustomerPassword = "string12"
            };

            Assert.ThrowsAsync<InvalidUserCredentialException>(async () => await _customerService.Login(login));
        }

        [Test]
        public async Task LoginCustomerIntanalServerError()
        {
            // Arrange
            DummyDB();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);

            // Act
            var login = new CustomerLoginDto()
            {
                CustomerEmail = "kiko@gmail.com",
                CustomerPassword = "string12"
            };
            
            // Assert
            Assert.ThrowsAsync<UnableToDoActionException>(async () => await _customerService.Login(login));
        }

        // Update phone number

        [Test]
        public async Task UpdatePhoneNumber()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };
            var result = await _customerService.Register(customer);

            // Act

            var updatePhone = await _customerService.UpdatePhone(1, "987654321");

            // Assert
            Assert.IsNotNull(updatePhone);
        }

        [Test]
        public async Task UpdatePhoneNumberError()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };
            var result = await _customerService.Register(customer);

            // Act
            Assert.ThrowsAsync<EntityNotFoundException<Customer>>(async () => await _customerService.UpdatePhone(2, "987654321"));
        }

        [Test]
        public async Task UpdatePhoneNumberIntenalServerError()
        {

            // Arrange

            DummyDB();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);


            // Act
            Assert.ThrowsAsync<UnableToDoActionException>(async () => await _customerService.UpdatePhone(2, "987654321"));
        }

        [Test]
        public async Task GetCustomer()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };
            var result = await _customerService.Register(customer);

            // Act
            var getCustomer = await _customerService.Get(1);

            // Assert
            Assert.IsNotNull(getCustomer);
        }

        [Test]
        public async Task GetCustomerError()
        {
            Assert.ThrowsAsync<EntityNotFoundException<Customer>>(async () => await _customerService.Get(2));
        }


        [Test]
        public async Task GetCustomerIntenalServerError()
        {
            // Arrange
            DummyDB();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);

            // Act
            Assert.ThrowsAsync<UnableToDoActionException>(async () => await _customerService.Get(2));
        }


    }
}
