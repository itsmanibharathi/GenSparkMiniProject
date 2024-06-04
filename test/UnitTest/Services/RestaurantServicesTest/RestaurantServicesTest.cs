using API.Exceptions;
using API.Models;
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

namespace UnitTest.Services.RestaurantServicesTest
{
    [TestFixture]
    internal class RestaurantServicesTest : ServicesTestBase
    {
        private RestaurantAuthService _restaurantAuthService;

        [SetUp]
        public void Setup()
        {
            SetupPasswordHashServices();
            SetupRestaurantTokenService();
            SetupRestaurantRepository();
            _restaurantAuthService = new RestaurantAuthService(_restaurantRepository,_passwordHashService,_restaurantTokenService,_mapper );
        }

        [Test]
        public async Task RegisterRestaurant()
        {
            // Arrange
            var res = new RestaurantRegisterDto()
            {
                Name = "KFC",
                Description = "KFC",
                Email = "kfc@gmail.com",
                Password = "password",
                Phone = "123456789",
                FssaiLicenseNumber = 1,
                Address = "KFC, Bangalore",
                Branch ="Main",
                State = "Karnataka",
                Zip = "",
                City = "Bangalore",
                AddressCode = AddressCode.a
            };

            // Act
            var result = await _restaurantAuthService.Register(res);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.RestaurantId ==1);
        }

        [Test]
        public async Task LoginRestaurant()
        {
            // Arrange
            var res = new RestaurantRegisterDto()
            {
                Name = "KFC",
                Description = "KFC",
                Email = "kfc@gmail.com",
                Password = "password",
                Phone = "123456789",
                FssaiLicenseNumber = 1,
                Address = "KFC, Bangalore",
                Branch = "Main",
                State = "Karnataka",
                Zip = "",
                City = "Bangalore",
                AddressCode = AddressCode.a
            };
            var result = await _restaurantAuthService.Register(res);


            // Act
            var login = new RestaurantLoginDto()
            {
                Email = "kfc@gmail.com",
                Password = "password"
            };
            var loginResult = await _restaurantAuthService.Login(login);

            // Assert
            Assert.IsNotNull(loginResult);
        }

        [Test]
        public async Task LoginRestauranInvalidUserExcption()
        {
            // Arrange
            var res = new RestaurantRegisterDto()
            {
                Name = "KFC",
                Description = "KFC",
                Email = "kfc@gmail.com",
                Password = "password",
                Phone = "123456789",
                FssaiLicenseNumber = 1,
                Address = "KFC, Bangalore",
                Branch = "Main",
                State = "Karnataka",
                Zip = "",
                City = "Bangalore",
                AddressCode = AddressCode.a
            };
            var result = await _restaurantAuthService.Register(res);


            // Act 
            var login = new RestaurantLoginDto()
            {
                Email = "kfc@kfc.in",
                Password = "password"
            };

            // Assert
            Assert.ThrowsAsync<InvalidUserCredentialException>(async () => await _restaurantAuthService.Login(login));
        }


        [Test]
        public async Task LoginRestauranInvalidUserExcption2()
        {
            // Arrange
            var res = new RestaurantRegisterDto()
            {
                Name = "KFC",
                Description = "KFC",
                Email = "kfc@gmail.com",
                Password = "password",
                Phone = "123456789",
                FssaiLicenseNumber = 1,
                Address = "KFC, Bangalore",
                Branch = "Main",
                State = "Karnataka",
                Zip = "",
                City = "Bangalore",
                AddressCode = AddressCode.a
            };
            var result = await _restaurantAuthService.Register(res);


            // Act 
            var login = new RestaurantLoginDto()
            {
                Email = "kfc@gmail.com",
                Password = "passwor"
            };

            // Assert
            Assert.ThrowsAsync<InvalidUserCredentialException>(async () => await _restaurantAuthService.Login(login));
        }
    }
}
