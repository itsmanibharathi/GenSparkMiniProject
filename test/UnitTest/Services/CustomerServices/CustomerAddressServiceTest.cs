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
        public async Task AddCustomerAddress()
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

        [Test]
        public async Task AddCustomerAddressDuplicateException()
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
            var result = await _customerAddressService.Add(customerAddress);

            // Assert
            Assert.ThrowsAsync<EntityAlreadyExistsException<CustomerAddress>>(() => _customerAddressService.Add(customerAddress));
        }


        [Test]
        public async Task AddCustomerAddressIntenalServerError()
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
            DummyDB();
            SetupCustomerAddressRepository();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);
            _customerAddressService = new CustomerAddressService(_customerAddressRepository, _mapper);

  
            // Assert
            Assert.ThrowsAsync<UnableToDoActionException>(() => _customerAddressService.Add(customerAddress));
        }


        [Test]
        public async Task GetCustomerAddress()
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

            var result = await _customerAddressService.Add(customerAddress);

            // Act
            var result2 = await _customerAddressService.Get(res.CustomerId, result.AddressId);

            // Assert
            Assert.NotNull(result2);
            Assert.AreEqual(result2.AddressId, result.AddressId);
        }

        [Test]
        public async Task GetCustomerAddressNotFoundError()
        {
            //Assert 

            Assert.ThrowsAsync<EntityNotFoundException<CustomerAddress>>(() => _customerAddressService.Get(1, 1));
        }

        [Test]
        public async Task GetCustomerAddressUnauthorizedAccessException()
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

            var result = await _customerAddressService.Add(customerAddress);

            // Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(() => _customerAddressService.Get(2, 1));
        }


        [Test]
        public async Task GetCustomerAddressInternalServerError()
        {
            // Arrange
            DummyDB();
            SetupCustomerAddressRepository();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);
            _customerAddressService = new CustomerAddressService(_customerAddressRepository, _mapper);

            //Assert 

            Assert.ThrowsAsync<UnableToDoActionException>(() => _customerAddressService.Get(1, 1));
        }


        [Test]
        async Task DeleteCustomerAddress()
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
            var result = await _customerAddressService.Add(customerAddress);


            // Act

            var result2 = await _customerAddressService.Delete(res.CustomerId, result.AddressId);

            // Assert

            Assert.AreEqual(result2, "Address Deleted Successfully");
        }

        [Test]
        public async Task DeleteCustomerAddressNotFoundError()
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
            var result = await _customerAddressService.Add(customerAddress);

            // Assert
            Assert.ThrowsAsync<EntityNotFoundException<CustomerAddress>>(() => _customerAddressService.Delete(2, 1));
        }

        //[Test]
        //public async Task DeleteCustomerAddressUnauthorizedAccessException()
        //{
        //    // Arrange
        //    var customer = new CustomerRegisterDto()
        //    {
        //        CustomerName = "Kiko",
        //        CustomerEmail = "kiko@gmail.com",
        //        CustomerPhone = "987654321",
        //        CustomerPassword = "string123",
        //    };
        //    var res = await _customerService.Register(customer);

        //    var customerAddress = new CustomerAddressDto()
        //    {
        //        CustomerId = res.CustomerId,
        //        Type = AddressType.Home,
        //        Code = AddressCode.a,
        //        City = "New York",
        //        State = "NY",
        //        Country = "USA",
        //        ZipCode = "10001"
        //    };
        //    var result = await _customerAddressService.Add(customerAddress);

        //    // Assert

        //    Assert.ThrowsAsync<UnauthorizedAccessException>(() => _customerAddressService.Delete(2, 1));
        //}

        [Test]
        public async Task DeleteCustomerAddressInternalServerError()
        {
            // Arrange
            DummyDB();
            SetupCustomerAddressRepository();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);
            _customerAddressService = new CustomerAddressService(_customerAddressRepository, _mapper);

            //Assert 

            Assert.ThrowsAsync<UnableToDoActionException>(() => _customerAddressService.Delete(1, 1));
        }

        [Test]
        public async Task GetCustomerAddressByCustomerId()
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
            var result = await _customerAddressService.Add(customerAddress);


            // Act
            var result2 = await _customerAddressService.Get(res.CustomerId);


            // Assert
            Assert.NotNull(result2);
            Assert.AreEqual(result2.Count(), 1);
        }


        [Test]
        public async Task GetCustomerAddressByCustomerIdNotFoundError()
        {
            //Assert 

            Assert.ThrowsAsync<EntityNotFoundException<CustomerAddress>>(() => _customerAddressService.Get(1));
        }

        [Test]
        public async Task GetCustomerAddressByCustomerIdInternalServerError()
        {
            // Arrange
            DummyDB();
            SetupCustomerAddressRepository();
            SetupPasswordHashServices();
            SetupCustomerRepository();
            SetupCustomerTokenService();
            _customerService = new CustomerService(_customerRepository, _passwordHashService, _customerTokenService, _mapper);
            _customerAddressService = new CustomerAddressService(_customerAddressRepository, _mapper);

            //Assert 

            Assert.ThrowsAsync<UnableToDoActionException>(() => _customerAddressService.Get(1));
        }
    }
}
