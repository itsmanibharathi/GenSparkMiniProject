using API.Exceptions;
using API.Models;
using API.Models.Enums;
using API.Repositories;
using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.EmployeeRepositoryTest
{
    [TestFixture]
    internal class EmployeeRepositoryTest : RepositoryTestBase
    {
        private EmployeeRepository _employeeRepository;

        [SetUp]
        public async Task Setup()
        {
            _employeeRepository = new EmployeeRepository(_context);
            await EmployeeSeedData();
        }

        [Test]
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
            var result = await _employeeRepository.AddAsync(employee);
            Assert.IsTrue(result.EmployeeId == 2);
        }
        [Test]
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
                await _employeeRepository.AddAsync(employee);
            }
            catch (EntityAlreadyExistsException<Employee> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
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
                await _employeeRepository.AddAsync(employee);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }


        [Test]
        public async Task GetEmployee()
        {
            var result = await _employeeRepository.GetAsync(1);
            Assert.IsTrue(result.EmployeeId == 1);
        }

        [Test]
        public async Task GetEmployeeNotFound()
        {
            try
            {
                await _employeeRepository.GetAsync(2);
            }
            catch (EntityNotFoundException<Employee> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetEmployeeIntenalserverError()
        {
            DummyDB();
            try
            {
                await _employeeRepository.GetAsync(1);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetEmployeeByEmail()
        {
            var result = await _employeeRepository.GetByEmailIdAsync("joe@gmail.com");
            Assert.IsNotNull(result.EmployeeAuth.Password);
        }

        [Test]
        public async Task GetEmployeeByEmailNotFound()
        {
            try
            {
                var result= await _employeeRepository.GetByEmailIdAsync("mani@gmail.com");
            }
            catch (EntityNotFoundException<Employee> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetEmployeeByEmailIntenalserverError()
        {
            DummyDB();
            try
            {
                await _employeeRepository.GetByEmailIdAsync("joe@gmail.com");
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetAllEmployee()
        {
            var result = await _employeeRepository.GetAsync();
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public async Task GetAllEmployeeIntenalserverError()
        {
            DummyDB();
            try
            {
                await _employeeRepository.GetAsync();
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }



        [Test]
        public async Task UpdateEmployee()
        {
            var employee = await _employeeRepository.GetAsync(1);
            employee.EmployeeName = "Ajay";
            var result = await _employeeRepository.UpdateAsync(employee);
        }

        [Test]
        public async Task UpdateEmployeeNotFound()
        {
            try
            {
                var employee = await _employeeRepository.GetAsync(2);
                employee.EmployeeId = 2;
                await _employeeRepository.UpdateAsync(employee);
            }
            catch (EntityNotFoundException<Employee> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateUnabletoDoAction()
        {
            try
            {
                var employee = await _employeeRepository.GetAsync(1);
                employee.EmployeeId = 2;
                await _employeeRepository.UpdateAsync(employee);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }


        [Test]
        public async Task UpdateEmployeeIntenalserverError()
        {
            DummyDB();
            try
            {
                var employee = await _employeeRepository.GetAsync(1);
                employee.EmployeeId = 1;
                await _employeeRepository.UpdateAsync(employee);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteEmployee()
        {
            await _employeeRepository.DeleteAsync(1);
        }

        [Test]
        public async Task DeleteEmployeeNotFound()
        {
            try
            {
                await _employeeRepository.DeleteAsync(2);
            }
            catch (EntityNotFoundException<Employee> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteEmployeeIntenalserverError()
        {
            DummyDB();
            try
            {
                await _employeeRepository.DeleteAsync(1);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
    }
}