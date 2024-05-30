using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using RepositoryTest;

namespace UnitTest.Repositories.CustomerRepositoriesTest
{
    internal class CustomerRepositoryTest : RepositoryTestBase
    {
        private CustomerRepository _repository;
        [SetUp]
        public async Task Setup()
        {
            _repository = new CustomerRepository(_context);
        }
        // Add Customer
        [Test]
        public async Task AddCustomer()
        {
            Customer customer = new Customer()
            {
                CustomerName = "Ludena",
                CustomerEmail = "ludena@gmail.com",
                CustomerPhone = "1234567890",
                CustomerAuth = new CustomerAuth()
                {
                    Password = "abc;xyz"
                }
            };
            foreach (var item in _context.Customers)
            {
                System.Console.WriteLine(item.CustomerName + " " + item.CustomerId);
            }
            var result = await _repository.Add(customer);
            Assert.IsTrue(result.CustomerId == 3);
        }
        [Test]
        public async Task AddCustomerDuplicateException()
        {
            Customer customer = new Customer()
            {
                CustomerName = "Mani",
                CustomerEmail = "mani@gmail.com",
                CustomerPhone = "1234567890"
            };
            try
            {
                var result = await _repository.Add(customer);
            }
            catch (DataDuplicateException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task AddCustomerIntenalErrorException()
        {
            DummyDB();
            Customer customer = new Customer()
            {
                CustomerName = "Ludena",
                CustomerEmail = "ludena@gmail.com",
                CustomerPhone = "1234567890"
            };
            try
            {
                var result = await _repository.Add(customer);
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }

        // Get Customer
        [Test]
        public async Task GetCustomer()
        {
            var result = await _repository.Get(1);
            Assert.That(result.CustomerName, Is.EqualTo("Mani"));
        }

        [Test]
        public async Task GetCustomerNotFoundException()
        {
            try
            {
                var result = await _repository.Get(3);
            }
            catch (CustomerNotFoundException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetCustomerIntenalErrorException()
        {
            DummyDB();
            try
            {
                var result = await _repository.Get(1);
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

        // Update Customer
        [Ignore("Test is not yet implemented")]
        [Test]
        public async Task UpdateCustomer()
        {
            DummyDB();
            Customer customer = new Customer()
            {
                CustomerId = 1,
                CustomerName = "Manibharathi"
            };
            var result = await _repository.Update(customer);
            Assert.That(result.CustomerName, Is.EqualTo("Manibharathi"));
        }
    }
}