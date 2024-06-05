using API.Exceptions;
using API.Models;
using API.Models.DTOs.EmployeeDto;
using API.Models.DTOs.RestaurantDto;
using API.Models.Enums;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Services.EmployeeServicesTest
{
    [TestFixture]
    internal class EmployeeServicesTest : ServicesTestBase
    {
        private IEmployeeService _employeeService;

        [SetUp]
        public void Setup()
        {
            SetupPasswordHashServices();
            SetupEmployeeRepository();
            SetupEmployeeTokenService();
            _employeeService = new EmployeeService(_employeeRepository, _passwordHashService, _employeeTokenService, _mapper);
        }

        [Test]
        public async Task RegisterEmployee()
        {
            // Arrange
            var res = new EmployeeRegisterDto()
            {
                EmployeeName = "Mani",
                EmployeeEmail = "mani@gmail.com",
                Password = "password",
                EmployeeAddress = "Bangalore",
                EmployeePhone = "123456789"
            };

            // Act
            var result = await _employeeService.Register(res);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.EmployeeId ==1);
        }

        [Test]
        public async Task LoginEmployee()
        {
            // Arrange
            var res = new EmployeeRegisterDto()
            {
                EmployeeName = "Mani",
                EmployeeEmail = "mani@gmail.com",
                Password = "password",
                EmployeeAddress = "Bangalore",
                EmployeePhone = "123456789"
            };
            
            
            var result = await _employeeService.Register(res);


            // Act
            var login = new EmployeeLoginDto()
            {
                EmployeeEmail = "mani@gmail.com",
                Password = "password",

            };
            var loginResult = await _employeeService.Login(login);

            // Assert
            Assert.IsNotNull(loginResult);
            Assert.IsNotNull(loginResult.Token);
        }

        [Test]
        public async Task LoginEmployeeInvalidUserExcption()
        {
            // Arrange
            var res = new EmployeeRegisterDto()
            {
                EmployeeName = "Mani",
                EmployeeEmail = "mani@gmail.com",
                Password = "password",
                EmployeeAddress = "Bangalore",
                EmployeePhone = "123456789"
            };


            var result = await _employeeService.Register(res);


            // Act
            var login = new EmployeeLoginDto()
            {
                EmployeeEmail = "mani@gmail.in",
                Password = "password"
            };
            
            // Assert
            Assert.ThrowsAsync<EntityNotFoundException<Employee>>(async () => await _employeeService.Login(login));
        }


        [Test]
        public async Task LoginEmployeeInvalidUserExcption2()
        {
            // Arrange
            var res = new EmployeeRegisterDto()
            {
                EmployeeName = "Mani",
                EmployeeEmail = "mani@gmail.com",
                Password = "password",
                EmployeeAddress = "Bangalore",
                EmployeePhone = "123456789"
            };


            var result = await _employeeService.Register(res);


            // Act
            var login = new EmployeeLoginDto()
            {
                EmployeeEmail = "mani@gmail.com",
                Password = "passwor"
            };

            // Assert
            Assert.ThrowsAsync<InvalidUserCredentialException>(async () => await _employeeService.Login(login));
        }
    }
}
