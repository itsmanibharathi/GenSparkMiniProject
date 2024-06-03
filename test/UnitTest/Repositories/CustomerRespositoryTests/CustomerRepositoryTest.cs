using API.Exceptions;
using API.Models;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.CustomerRespositoryTests
{
    internal class CustomerRepositoryTest : RepositoryTestBase
    {
        private CustomerRepository _customerRepository;

        [SetUp]
        public async Task Setup()
        {
            _customerRepository = new CustomerRepository(_context);
            await CustomerSeedData();
        }

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
            var result = await _customerRepository.AddAsync(customer);
            Assert.IsTrue(result.CustomerId == 3);
        }

        [Test]
        public async Task AddDuplicateCustomer()
        {
            Customer customer = new Customer()
            {
                CustomerName = "Mani",
                CustomerEmail = "mani@gmail.com",
                CustomerPhone = "1234567890"
            };

            try
            {
                await _customerRepository.AddAsync(customer);
            }
            catch (EntityAlreadyExistsException<Customer> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public async Task AddCustomerInternalServerException()
        {
            DummyDB();
            Customer customer = new Customer()
            {
                CustomerName = "Mani",
                CustomerEmail = "mani@gmail.com",
                CustomerPhone = "1234567890"
            };

            try
            {
                await _customerRepository.AddAsync(customer);
            }
            catch (EntityNotFoundException<Customer> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public async Task GetCustomer()
        {
            var result = await _customerRepository.GetAsync(1);
            Assert.IsTrue(result.CustomerId == 1);
        }
        [Test]
        public async Task GetCustomerNotFoundException()
        {
            try
            {
                var result = await _customerRepository.GetAsync(3);
            }
            catch (EntityNotFoundException<Customer> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetCustomerInternalServerException()
        {
            DummyDB();
            try
            {
                var result = await _customerRepository.GetAsync(1);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetCustomerByEmail()
        {
            var result = await _customerRepository.GetByEmailId("mani@gmail.com");
            Assert.IsNotNull(result.CustomerAuth.Password);
        }

        [Test]
        public async Task GetCustomerByEmailNotFoundException()
        {
            try
            {
                var result = await _customerRepository.GetByEmailId("kali@gmail.com");
            }
            catch (EntityNotFoundException<Customer> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetCustomerByEmailInternalServerException()
        {
            DummyDB();
            try
            {
                var result = await _customerRepository.GetByEmailId("mani@gmail.com");
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetAllCustomer()
        {
            var result = await _customerRepository.GetAsync();
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public async Task GetAllCustomerIntenalServerException()
        {
            DummyDB();
            try
            {
                var result = await _customerRepository.GetAsync();
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateCustomer()
        {
            var customer = await _customerRepository.GetAsync(1);
            customer.CustomerName = "Mani M";
            var result = await _customerRepository.UpdateAsync(customer);
            Assert.IsTrue(result.CustomerName == "Mani M");
        }

        [Test]
        public async Task UpdateCustomerNotFountException()
        {
            try
            {
                var customer = await _customerRepository.GetAsync(1);
                customer.CustomerName = "Mani M";
                var result = await _customerRepository.UpdateAsync(customer);
            }
            catch (EntityNotFoundException<Customer> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateCustomerUnableToDoActionException()
        {
            DummyDB();
            var customer = await _customerRepository.GetAsync(1);
            customer.CustomerId = 3;
            try
            {
                var result = await _customerRepository.UpdateAsync(customer);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }


        [Test]
        public async Task UpdateCustomerInternalServerException()
        {
            DummyDB();
            var customer = await _customerRepository.GetAsync(1);
            customer.CustomerName = "Mani M";
            try
            {
                var result = await _customerRepository.UpdateAsync(customer);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteCustomer()
        {
            var result = await _customerRepository.DeleteAsync(1);
            Assert.IsTrue(result);
            try
            {
                var res = await _customerRepository.GetAsync(1);
            }
            catch (EntityNotFoundException<Customer> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteCustomerNotFound()
        {
            try
            {
                var result = await _customerRepository.DeleteAsync(3);
            }
            catch (EntityNotFoundException<Customer> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteCustomerIntenalServerError()
        {
            DummyDB();
            try
            {
                var result = await _customerRepository.DeleteAsync(1);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
    }
}
