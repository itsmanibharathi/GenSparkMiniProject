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
                CustomerPhone = "1234567890"
            };
            foreach (var item in _context.Customers)
            {
                System.Console.WriteLine(item.CustomerName + " " + item.CustomerId );
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
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                .UseInMemoryDatabase("dummyDB");
            _repository = new CustomerRepository(new DBGenSparkMinirojectContext(optionsBuilder.Options));
            Customer customer = new Customer()
            {
                CustomerName = "Ludena",
                CustomerEmail = "ludena@gmail.com",
                CustomerPhone = "1234567890"
            };
            foreach (var item in _context.Customers)
            {
                System.Console.WriteLine(item.CustomerName + " " + item.CustomerId);
            }
            try
            {
                var result = await _repository.Add(customer);
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }
    }
}