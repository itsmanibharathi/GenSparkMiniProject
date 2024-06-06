using API.Exceptions;
using API.Models;
using API.Models.DTOs.RestaurantDto;
using API.Models.Enums;
using API.Services;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Services.RestaurantServicesTest
{
    [TestFixture]
    internal class RestaurantProductServiceTest : ServicesTestBase  
    {
        private IRestaurantProductService _restaurantProductService;

        [SetUp]
        public async Task Setup()
        {
            await base.Setup();

            await base.RestaurantSeedData();

            SetupRestaurantProductRepository();
            _restaurantProductService = new RestaurantProductService(_restaurantProductRepository, _mapper);
        }

        [Test]
        public async Task AddProduct()
        {

            // Act

            var product = new RestaurantProductDto()
            {
                ProductName = "Pizza",
                ProductDescription = "Pizza",
                ProductPrice = 100,
                ProductCategories = ProductCategory.Food,
                ProductAvailable = true,
                RestaurantId = 1
            };
            var response = await _restaurantProductService.Add(product);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.ProductId == 3);
        }
        //[Test]
        //public async Task AddProductRestaurantNotFound()
        //{
        //    // Act

        //    var product = new RestaurantProductDto()
        //    {
        //        ProductName = "Pizza",
        //        ProductDescription = "Pizza",
        //        ProductPrice = 100,
        //        ProductCategories = ProductCategory.Food,
        //        ProductAvailable = true,
        //        RestaurantId = 1
        //    };

        //    // assert

        //    Assert.ThrowsAsync<EntityNotFoundException<Restaurant>>(async () => await _restaurantProductService.Add(product));
        //}
    }
}
