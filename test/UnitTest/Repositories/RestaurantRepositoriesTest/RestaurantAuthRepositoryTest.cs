using API.Exceptions;
using API.Repositories;
using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.RestaurantRepositoriesTest
{
    internal class RestaurantAuthRepositoryTest : RepositoryTestBase
    {
        private IRestaurantAuthRepository _repository;

        [SetUp]
        public async Task Setup()
        {
            _repository = new RestaurantAuthRepository(_context);
        }

        // Get the restaurant auth details
        [Test]
        public async Task GetRestaurant()
        {
            var result = await _repository.Get("kfc@gmail.com");
            Assert.IsNotNull(result.RestaurantAuth);
        }

        [Test]
        public async Task GetRestaurantInvalidUserCredentialException()
        {
            try
            {
                var result = await _repository.Get("kfc@co.in");
            }
            catch (InvalidUserCredentialException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetRestaurantIntenalErrorException()
        {
            DummyDB();
            try
            {
                var result = await _repository.Get("kfc@gmail.com");
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }
    }
}
