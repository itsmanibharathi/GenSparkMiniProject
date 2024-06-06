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
    internal class CustomerAddressServiceTest : ServicesTestBase
    {
        private ICustomerService _customerService;
        private ICustomerAddressService _customerAddressService;

        [SetUp]
        public async Task Setup()
        {
            await base.Setup();
            SetupCustomerAddressRepository();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);
            _customerAddressService = new CustomerAddressService(_customerAddressRepository, _mapper);
        }

        [Test]
        public async Task AddCustomerAddressAsync_WhenCalled_ReturnsCustomerAddress()
        {
            // Arrange
            var customer = new CustomerRegisterDto()
            {
                CustomerName = "Kiko",
                CustomerEmail = "kiko@gmail.com",
                CustomerPhone = "987654321",
                CustomerPassword = "string123",
            };
            var res = await _customerService.Register(customer);

            var customerAddress = new CustomerAddressDto()
            {
                CustomerId = res.CustomerId,
                Type = AddressType.Home,
                Code = AddressCode.a,
                City = "New York",
                State = "NY",
                Country = "USA",
                ZipCode = "10001"
            };


            // Act
            var result  = await _customerAddressService.Add(customerAddress);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(result.CustomerId, customerAddress.CustomerId);
            Assert.AreEqual(result.AddressId,1);
        }
    }
}
