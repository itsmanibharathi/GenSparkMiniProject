using API.Context;
using API.Exceptions;
using API.Models;
using API.Models.Enums;
using API.Repositories;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.CustomerRepositoriesTest
{
    [TestFixture]
    internal class CustomerAddressRepositoryTest : RepositoryTestBase
    {
        private ICustomerAddressRepository _repository;

        [SetUp]
        public async Task Setup()
        {
            _repository = new CustomerAddressRepository(_context);
        }

        // Add Customer Address
        [Test , Order(1) ]
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


            var result = await _repository.Add(customerAddress);
            

            Assert.IsTrue(result.AddressId == 3);
        }

        [Test , Order(2) ] 
        public async Task AddCustomerAddressDuplicateException()
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
                var result = await _repository.Add(customerAddress);
            }
            catch (DataDuplicateException)
            {
                Assert.Pass();
            }
        }

        [Test, Order(3)]
        public async Task AddCustomerAddressIntenalErrorException()
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
                var result = await _repository.Add(customerAddress);
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }


        // Get Customer Address
        [Test , Order(4)]
        public async Task GetCustomerAddress()
        {
            var result = await _repository.Get(1,1);
            Assert.IsTrue(result.AddressId == 1);
        }

        [Test, Order(5)]
        public async Task GetCustomerAddressNotFoundException()
        {
            try
            {
                var result = await _repository.Get(1,10);
            }
            catch (CustomerAddressNotFoundException)
            {
                Assert.Pass();
            }
        }

        [Test, Order(6)]
        public async Task GetCustomerAddressInternalErrorException()
        {
            DummyDB();
            try
            {
                var result = await _repository.Get(1,1);
            }
            catch (CustomerAddressNotFoundException)
            {
                Assert.Pass();
            }
        }

        // Get Customer Address by Customer Id
        [Test, Order(7)]
        public async Task GetCustomerAddressByCustomerId()
        {
            var result = await _repository.Get(1);
            Assert.IsTrue(result.Any());
        }

        [Test, Order(8)]
        public async Task GetCustomerAddressByCustomerIdNotFoundException()
        {
            try
            {
                var result = await _repository.Get(10);
            }
            catch (CustomerAddressNotFoundException)
            {
                Assert.Pass();
            }
        }

        [Test, Order(9)]
        public async Task GetCustomerAddressByCustomerIdInternalErrorException()
        {
            DummyDB();
            try
            {
                var result = await _repository.Get(1);
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }

        // Delete Customer Address
        [Test , Order(10)]
        public async Task DeleteCustomerAddress()
        {
            var result = await _repository.Delete(1,1);
            Assert.IsTrue(result);
        }

        [Test, Order(11)]
        public async Task DeleteCustomerAddressFalier()
        {
            try
            {
                var result = await _repository.Delete(1,1);
            }
            catch (CustomerAddressNotFoundException)
            {
                Assert.Pass();
            }
        }

        [Test, Order(12)]
        public async Task DeleteCustomerAddressInternalErrorException()
        {
            DummyDB();
            try
            {
                var result = await _repository.Delete(1,1);
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }
    }
}
