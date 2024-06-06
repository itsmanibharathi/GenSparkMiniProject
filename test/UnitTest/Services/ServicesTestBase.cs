﻿using API.Models;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using API.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Repositories;
using API.Repositories;

namespace UnitTest.Services
{
    internal class ServicesTestBase : RepositoryTestBase
    {
        public IRestaurantOrderRepository _restaurantOrderRepository;
        public IRestaurantRepository _restaurantRepository;
        public IPasswordHashService _passwordHashService;
        public ITokenService<Restaurant> _restaurantTokenService;
        public IEmployeeRepository _employeeRepository;
        public ITokenService<Employee> _employeeTokenService;

        public ICustomerRepository _customerRepository;
        public ITokenService<Customer> _customerTokenService;

        public ICustomerAddressRepository _customerAddressRepository;
        public IRestaurantProductRepository _restaurantProductRepository;

        public IMapper _mapper;

        [SetUp]
        public async Task Setup()
        {
            // Calling the base setup method
            await base.Setup();
            // Creating a mapper configuration
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();
        }
        public void SetupPasswordHashServices()
        {
            _passwordHashService = new PasswordHashService();   
        }

        public void SetupRestaurantTokenService()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["Jwt:RestaurantSecret"]).Returns(" Restaurant This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            _restaurantTokenService = new RestaurantTokenService(configuration.Object);
        }
        public void SetupEmployeeTokenService()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["Jwt:EmployeeSecret"]).Returns(" Employee This  is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
        }

        public void SetupCustomerTokenService()
        {
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x["JWT:CustomerSecret"]).Returns("Customer This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            _customerTokenService = new CustomerTokenService(configuration.Object);
        }

        public void SetupRestaurantRepository()
        {
            _restaurantRepository = new RestaurantRepository(_context);
        }

        public void SetupEmployeeRepository()
        {
            _employeeRepository = new EmployeeRepository(_context);
        }

        public void SetupCustomerRepository()
        {
            _customerRepository = new CustomerRepository(_context);
        }

        public void SetupCustomerAddressRepository()
        {
            _customerAddressRepository = new CustomerAddressRespository(_context);
        }

        public void SetupRestaurantProductRepository()
        {
            _restaurantProductRepository = new RestaurantProductRepository(_context);
        }

        [TearDown]
        public async Task TearDown()
        {
            await base.TearDown();
        }

    }

}
