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

namespace UnitTest.Repositories.RestaurantRepositoryTests
{
    [TestFixture]
    internal class RestaurantRepositoryTest : RepositoryTestBase
    {
        private RestaurantRepository _restaurantRepository;

        [SetUp]
        public async Task Setup()
        {
            _restaurantRepository = new RestaurantRepository(_context);
            await RestaurantSeedData();
        }

        // AddRestaurant
        [Test]
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

            var result = await _restaurantRepository.AddAsync(restaurant);
            Assert.IsTrue(result.RestaurantId == 3);
        }
        // AddDuplicateRestaurant
        [Test]
        public async Task AddDuplicateRestaurant()
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
                var result = await _restaurantRepository.AddAsync(restaurant);
            }
            catch (EntityAlreadyExistsException<Restaurant> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        // AddRestaurantIntenalErrorException

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
                var result = await _restaurantRepository.AddAsync(restaurant);
            }
            catch (UnableToDoActionException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetRestaurant()
        {
            var result = await _restaurantRepository.GetAsync(1);
            Assert.IsTrue(result.RestaurantId == 1);
        }

        [Test]
        public async Task GetRestaurantNotFoundException()
        {
            try
            {
                var result = await _restaurantRepository.GetAsync(3);
            }
            catch (EntityNotFoundException<Restaurant> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetRestaruantIntenalServer()
        {
            DummyDB();
            try
            {
                var result = await _restaurantRepository.GetAsync(1);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetRestaurantByEmail()
        {
            var result = await _restaurantRepository.GetByEmailId("kfc@gmail.com");
            Assert.IsTrue(result.RestaurantId == 1);
        }

        [Test]
        public async Task GetRestaurantByEmailNotFoundException()
        {
            try
            {
                var result = await _restaurantRepository.GetByEmailId("kfc@kfc.com");
            }
            catch (EntityNotFoundException<Restaurant> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
        [Test]
        public async Task GetRestaurantByEmailIntenalServerError()
        {
            DummyDB();
            try
            {
                var result = await _restaurantRepository.GetByEmailId("kfc@gmail.com");

            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task GetAllRestaurant()
        {
            var result = await _restaurantRepository.GetAsync();
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public async Task GetAllRestaurantIntenalServerError()
        {
            DummyDB();
            try
            {
                var result = await _restaurantRepository.GetAsync();
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateRestaurant()
        {
            var restaurant = await _restaurantRepository.GetAsync(1);
            restaurant.Name = "KFC";
            var result = await _restaurantRepository.UpdateAsync(restaurant);
            Assert.IsTrue(result.Name == "KFC");
        }


        [Test]
        public async Task UpdateRestaurantNotFound()
        {
            try
            {
                var restaurant = await _restaurantRepository.GetAsync(3);
            }
            catch (EntityNotFoundException<Restaurant> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateUnableToDoAction()
        {
            try
            {
                var restaurant = await _restaurantRepository.GetAsync(1);
                restaurant.RestaurantId = 2;
                var result = await _restaurantRepository.UpdateAsync(restaurant);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }


        [Test]
        public async Task UpdateRestaurantIntenalServerError()
        {
            DummyDB();
            try
            {
                var restaurant = await _restaurantRepository.GetAsync(1);
                restaurant.Name = "KFC";
                var result = await _restaurantRepository.UpdateAsync(restaurant);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }


        [Test]
        public async Task DeleteRestaurant()
        {
            var result = await _restaurantRepository.DeleteAsync(1);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteRestaurantNotFound()
        {
            try
            {
                var result = await _restaurantRepository.DeleteAsync(3);
            }
            catch (EntityNotFoundException<Restaurant> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task DeleteRestaurantIntenalServerError()
        {
            DummyDB();
            try
            {
                var result = await _restaurantRepository.DeleteAsync(1);
            }
            catch (UnableToDoActionException ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
    }
}
