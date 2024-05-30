using API.Exceptions;
using API.Models;
using API.Models.Enums;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.EmployeeRepositoriesTest
{
    [TestFixture]
    internal class EmployeeRepositotyTest : RepositoryTestBase
    {
        private EmployeeRepository _repository;

        [SetUp]
        public async Task Setup()
        {
            _repository = new EmployeeRepository(_context);
        }

        // Add Employee
        [Test, Order(1)]
        public async Task AddEmployee()
        {
            Employee employee = new Employee
            {
                EmployeeName = "Ajay",
                EmployeeEmail = "ajay@gmail.com",
                EmployeePhone = "123456789",
                EmployeeAddress = "123",
                AddressCode = AddressCode.v,
                EmployeeAuth = new EmployeeAuth
                {
                    Password = "abc;xyz"
                }
            };
            var result =await _repository.Add(employee);
            Console.WriteLine(result.EmployeeId);
            Assert.IsTrue(result.EmployeeId == 2);
        }

        // Add Duplicate Employee
        [Test, Order(2)]
        public async Task AddDuplicateEmployee()
        {
            Employee employee = new Employee
            {
                EmployeeName = "Joe",
                EmployeeEmail = "joe@gmail.com",
                EmployeePhone = "123456789",
                EmployeeAddress = "123",
                AddressCode = AddressCode.a,
                EmployeeAuth = new EmployeeAuth
                {
                    Password = "abc;xyz"
                }
            };
            try
            {
                await _repository.Add(employee);
            }
            catch (DataDuplicateException ex)
            {
                Assert.Pass();
            }
        }

        [Test, Order(3)]
        public async Task AddIntenalserverError()
        {
            DummyDB();
            Employee employee = new Employee
            {
                EmployeeName = "Ajay",
                EmployeeEmail = "ajay@gmail.com",
                EmployeePhone = "123456789",
                EmployeeAddress = "123",
                AddressCode = AddressCode.v,
                EmployeeAuth = new EmployeeAuth
                {
                    Password = "abc;xyz"
                }
            };
            try
            {
                await _repository.Add(employee);
            }
            catch (UnableToDoActionException ex)
            {
                Assert.Pass();
            }
        }


    }
}
