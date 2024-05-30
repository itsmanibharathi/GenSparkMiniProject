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

namespace UnitTest.Repositories.RestaurantRepositoriesTest
{
    [TestFixture]
    internal class RestaurantRepositoryTest : RepositoryTestBase
    {
        private IRepository<int,Restaurant> _repository;

        [SetUp]
        public async Task Setup()
        {
            _repository = new RestaurantRepository(_context);
        }

        // Add Restaurant
        [Test, Order(1)]
        public async Task AddRestaurant()
        {
            Restaurant restaurant = new Restaurant()
            {
                Name = "MCdonald's",
                Description = "Fast Food",
                Phone = "123456789",
                Email = "mcdonalds@gmail.com",
                Branch = "Erode Main",
                Address = "Erode",
                City = "Erode",
                State = "Tamil Nadu",
                Zip = "638001",
                AddressCode = AddressCode.d,
                FssaiLicenseNumber = 123
            };
            var result = await _repository.Add(restaurant);
            Assert.IsTrue(result.RestaurantId == 3);
        }

        [Test]
        public async Task AddRestaurantDuplicateException1()
        {
            Restaurant restaurant = new Restaurant()
            {
                Name = "KFC",
                Description = "Fast Food",
                Phone = "123456789",
                Email = "kfc@gmail.com",
                Branch = "Erode Main",
                Address = "Erode",
                City = "Erode",
                AddressCode = AddressCode.d,
                FssaiLicenseNumber = 1
            };
            try
            {
                var result = await _repository.Add(restaurant);
            }
            catch (DataDuplicateException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task AddRestaurantDuplicateException2()
        {
            Restaurant restaurant = new Restaurant()
            {
                Name = "MCdonald's",
                Description = "Fast Food",
                Phone = "123456789",
                Email = "mcdonalds@gmail.com",
                Branch = "Erode Main",
                Address = "Erode",
                City = "Erode",
                AddressCode = AddressCode.d,
                FssaiLicenseNumber = 12
            };
            try
            {
                var result = await _repository.Add(restaurant);
            }
            catch (DataDuplicateException)
            {
                Assert.Pass();
            }
        }


        [Test]
        public async Task AddRestaurantIntenalErrorException()
        {
            DummyDB();
            Restaurant restaurant = new Restaurant()
            {
                Name = "KFC",
                Description = "Fast Food",
                Phone = "123456789",
                Email = "kfc1@gmail.com",
                Branch = "Erode Main",
                Address = "Erode",
                City = "Erode",
                AddressCode = AddressCode.d,
                FssaiLicenseNumber = 123456
            };
            try
            {
                var result = await _repository.Add(restaurant);
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetRestaurant()
        {
            var result = await _repository.Get(1);
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetRestaurantNotFoundException()
        {
            try
            {
                var result = await _repository.Get(3);
            }
            catch (RestaurantNotFoundException)
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
                var result = await _repository.Get(1);
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }

        // get restaurants
        [Test]
        public async Task GetRestaurants()
        {
            var result = await _repository.Get();
            Assert.IsNotNull(result);
        }
        [Test]
        public async Task GetRestaurantsEmptyDatabaseException()
        {
            EmptyDB();
            try
            {
                var result = await _repository.Get();
            }
            catch (EmptyDatabaseException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetRestaurantsIntenalErrorException()
        {
            DummyDB();
            try
            {
                var result = await _repository.Get();
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }

        // Update Restaurant
        [Test]
        public async Task UpdateRestaurant()
        {
            Restaurant restaurant = await _repository.Get(1);
            restaurant.Description = "Fast Foods";
            var result = await _repository.Update(restaurant);
            Assert.IsTrue(result.Description == "Fast Foods");
        }

        [Test]
        public async Task UpdateRestaurantNotFoundException()
        {
            try
            {
                Restaurant restaurant = await _repository.Get(10);
                var result = await _repository.Update(restaurant);
            }
            catch (RestaurantNotFoundException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateRestaurantIntenalErrorException()
        {
            DummyDB();
            try
            {
                Restaurant restaurant = await _repository.Get(1);
                var result = await _repository.Update(restaurant);
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }




    }
}
