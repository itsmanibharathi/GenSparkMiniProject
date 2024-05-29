using API.Context;
using API.Exceptions;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using RepositoryTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.CustomerRepositoriesTest
{
    internal class CustomerAuthRepositoryTest : RepositoryTestBase
    {
        private CustomerAuthRepository _repository;
        [SetUp]
        public async Task Setup()
        {
            _repository = new CustomerAuthRepository(_context);
        }

        // Get the customer auth details
        [Test]
        public async Task GetCustomer()
        {
            var result = await _repository.Get("mani@gmail.com");
            
            Assert.IsNotNull(result.CustomerAuth);
        }
        [Test]
        public async Task GetCustomerInvalidUserCredentialException()
        {
            try
            {
                var result = await _repository.Get("ludena@gmail.com");
            }
            catch (InvalidUserCredentialException)
            {
                Assert.Pass();
            }
        }

        [Ignore("Internal Server Error")]
        [Test]
        public async Task GetCustomerIntenalErrorException()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                    .UseInMemoryDatabase("dummyDB");
            _repository = new CustomerAuthRepository(new DBGenSparkMinirojectContext(optionsBuilder.Options));
            try
            {
                var result = await _repository.Get("mani@gmail.com");
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }
    }
}
