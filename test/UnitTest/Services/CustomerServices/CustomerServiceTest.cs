using API.Exceptions;
using API.Models.DTOs.CustomerDto;
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
    }
}
