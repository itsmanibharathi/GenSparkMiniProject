using API.Models;
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
            configuration.Setup(x => x["Jwt:RestaurantSecret"]).Returns("This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing");
            _restaurantTokenService = new RestaurantTokenService(configuration.Object);
        }
        public void SetupRestaurantRepository()
        {
            _restaurantRepository = new RestaurantRepository(_context);
        }

        [TearDown]
        public async Task TearDown()
        {
            await base.TearDown();
        }

    }

}
