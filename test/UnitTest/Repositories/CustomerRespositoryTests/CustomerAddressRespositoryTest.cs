using API.Exceptions;
using API.Models;
using API.Models.Enums;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.CustomerRespositoryTests
{
    internal class CustomerAddressRespositoryTest : RepositoryTestBase
    {
        private CustomerAddressRespository _customerAddressRespository;

        [SetUp]
        public async Task Setup()
        {
            _customerAddressRespository = new CustomerAddressRespository(_context);
            await CustomerSeedData();
        }

        [Test]
        public async Task AddCustomerAddress()
        {
            CustomerAddress customerAddress = new CustomerAddress()
            {
                CustomerId = 1,
                Type = AddressType.Work,
                Code = AddressCode.h,
                City = "xxx",
                State = "yyy"
            };
            var result = await _customerAddressRespository.AddAsync(customerAddress);
            Assert.IsTrue(result.AddressId == 3);
        }

        [Test]
        public async Task AddDuplicateCustomerAddress()
        {
            CustomerAddress customerAddress = new CustomerAddress()
            {
                CustomerId = 2,
                Type = AddressType.Work,
                Code = AddressCode.z,
                City = "xxx",
                State = "yyy"
            };

            try
            {
                await _customerAddressRespository.AddAsync(customerAddress);
            }
            catch (EntityAlreadyExistsException<CustomerAddress> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public async Task AddCustomerAddressInternalServerException()
        {
            DummyDB();
            CustomerAddress customerAddress = new CustomerAddress()
            {
                Type = AddressType.Home,
                Code = AddressCode.a,
                City = "xxx",
                State = "yyy"
            };

            try
            {
                await _customerAddressRespository.AddAsync(customerAddress);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public async Task GetCustomerAddress()
        {
            var result = await _customerAddressRespository.GetAsync(1);
            Assert.IsTrue(result.AddressId == 1);
        }
        [Test]
        public async Task GetCustomerAddressNotFoundException()
        {
            try
            {
                var result = await _customerAddressRespository.GetAsync(3);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetCustomerAddressInternalServerException()
        {
            DummyDB();
            try
            {
                var result = await _customerAddressRespository.GetAsync(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetCustomerAddressByCustomerId()
        {
            var result = await _customerAddressRespository.GetByCustomerIdAsync(1);
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public async Task GetCustomerAddressByCustomerIdNotFoundException()
        {
            try
            {
                var result = await _customerAddressRespository.GetByCustomerIdAsync(3);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetCustomerAddressByCustomerIdInternalServerException()
        {
            DummyDB();
            try
            {
                var result = await _customerAddressRespository.GetByCustomerIdAsync(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetAllCutomerAddress()
        {
            var result = await _customerAddressRespository.GetAsync();
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public async Task GetAllCutomerAddressInternalServerException()
        {
            DummyDB();
            try
            {
                var result = await _customerAddressRespository.GetAsync();
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateCustomerAddress()
        {
            var customerAddress = await _customerAddressRespository.GetAsync(1);
            customerAddress.City = "Chennai";
            var result = await _customerAddressRespository.UpdateAsync(customerAddress);
            Assert.IsTrue(result.City == "Chennai");
        }

        [Test]
        public async Task UpdateCustomerAddressNotFoundException()
        {
            try
            {
                var customerAddress = await _customerAddressRespository.GetAsync(3);
                customerAddress.City = "Chennai";
                var result = await _customerAddressRespository.UpdateAsync(customerAddress);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateCustomerAddressUnableToDoActionException()
        {
            var customerAddress = await _customerAddressRespository.GetAsync(1);
            customerAddress.AddressId = 3;
            try
            {
                var result = await _customerAddressRespository.UpdateAsync(customerAddress);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateCustomerAddressIntenalServerErrorException()
        {
            DummyDB();
            var customerAddress = await _customerAddressRespository.GetAsync(1);
            try
            {
                var result = await _customerAddressRespository.UpdateAsync(customerAddress);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }


        [Test]
        public async Task DeleteCustomerAddress()
        {
            var result = await _customerAddressRespository.DeleteAsync(1, 1);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteCustomerAddressNotFoundException()
        {
            try
            {
                var result = await _customerAddressRespository.DeleteAsync(1, 3);
            }
            catch (EntityNotFoundException<CustomerAddress> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteCustomerAddressUnableToDoActionException()
        {
            DummyDB();
            try
            {
                var result = await _customerAddressRespository.DeleteAsync(1, 1);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
    }
}
