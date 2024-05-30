using API.Exceptions;
using API.Models;
using API.Models.Enums;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.RestaurantRepositoriesTest
{
    [TestFixture]
    internal class RestructureProductRepositoryTest : RepositoryTestBase
    {
        private ProductRepository _repository;

        [SetUp]
        public async Task Setup()
        {
            _repository = new ProductRepository(_context);
        }

        // Add Product
        [Test, Order(1)]
        public async Task AddProduct()
        {
            Product product = new Product()
            {
                RestaurantId = 1,
                ProductName = "Pizza",
                ProductDescription = "Pizza",
                ProductPrice = 100,
                ProductCategories = ProductCategory.Food
            };

            var result = await _repository.Add(product);

            Assert.IsTrue(result.ProductId == 3);
        }

        // Add Duplicate Product
        [Test , Order(2)]
        public async Task AddDuplicateProduct()
        {
            Product product = new Product()
            {
                RestaurantId = 1,
                ProductName = "Chicken Rise",
                ProductDescription = "Chicken",
                ProductPrice = 100,
                ProductCategories = ProductCategory.Food
            };

            try
            {
                await _repository.Add(product);
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
            Product product = new Product()
            {
                RestaurantId = 1,
                ProductName = "Pizza",
                ProductDescription = "Pizza",
                ProductPrice = 100,
                ProductCategories = ProductCategory.Food
            };
            try
            {
                await _repository.Add(product);
            }
            catch (UnableToDoActionException ex)
            {
                Assert.AreEqual("Unable to Insert the new Product", ex.Message);
            }
        }

        // Delete Product
        [Test, Order(4)]
        public async Task DeleteProduct()
        {
            var result = await _repository.Delete(1);
            Assert.IsTrue(result);
        }

        // Delete Product Not Found
        [Test, Order(5)]
        public async Task DeleteProductNotFound()
        {
            try
            {
                await _repository.Delete(1);
            }
            catch (ProductNotFoundException ex)
            {
                Assert.Pass();
            }
        }

        // Delete Product Internal Server Error

        [Test, Order(6)]
        public async Task DeleteProductInternalServerError()
        {
            DummyDB();
            try
            {
                await _repository.Delete(1);
            }
            catch (UnableToDoActionException ex)
            {
                Assert.AreEqual("Unable to delete the Product", ex.Message);
            }
        }

        // Get Product

        [Test, Order(7)]
        public async Task GetProduct()
        {
            var result = await _repository.Get(1);
            Assert.IsTrue(result.ProductId == 1);
        }

        // Get Product Not Found
        [Test, Order(8)]
        public async Task GetProductNotFound()
        {
            try
            {
                await _repository.Get(1);
            }
            catch (ProductNotFoundException ex)
            {
                Assert.Pass();
            }
        }

        // Get Product Internal Server Error
        [Test, Order(9)]
        public async Task GetProductInternalServerError()
        {
            DummyDB();
            try
            {
                await _repository.Get(1);
            }
            catch (UnableToDoActionException ex)
            {
                Assert.AreEqual("Unable to get the Product", ex.Message);
            }
        }

        // Update Product
        [Test, Order(10)]
        public async Task UpdateProduct()
        {
            Product product = new Product()
            {
                ProductId = 1,
                RestaurantId = 1,
                ProductName = "Pizza",
                ProductDescription = "Pizza",
                ProductPrice = 100,
                ProductCategories = ProductCategory.Food
            };

            var result = await _repository.Update(product);
            Assert.IsTrue(result.ProductId == 1);
        }

        // Update Product Not Found
        [Test, Order(11)]
        public async Task UpdateProductNotFound()
        {
            Product product = new Product()
            {
                ProductId = 1,
                RestaurantId = 1,
                ProductName = "Pizza",
                ProductDescription = "Pizza",
                ProductPrice = 100,
                ProductCategories = ProductCategory.Food
            };

            try
            {
                await _repository.Update(product);
            }
            catch (ProductNotFoundException ex)
            {
                Assert.Pass();
            }
        }

        // Update Product Internal Server Error
        [Test, Order(12)]
        public async Task UpdateProductInternalServerError()
        {
            DummyDB();
            Product product = new Product()
            {
                ProductId = 1,
                RestaurantId = 1,
                ProductName = "Pizza",
                ProductDescription = "Pizza",
                ProductPrice = 100,
                ProductCategories = ProductCategory.Food
            };

            try
            {
                await _repository.Update(product);
            }
            catch (UnableToDoActionException ex)
            {
                Assert.AreEqual("Unable to update the Product", ex.Message);
            }
        }

        // Get All Products
        [Test, Order(13)]
        public async Task GetAllProducts()
        {
            var result = await _repository.Get();
            Assert.IsTrue(result.Count() == 2);
        }
        [Test]
        public async Task GetAllProductsEmpty()
        {
            DummyDB();
            try
            {
                await _repository.Get();
            }
            catch (EmptyDatabaseException ex)
            {
                Assert.Pass();
            }
        }
        [Test]
        public async Task GetAllProductsInternalServerError()
        {
            DummyDB();
            try
            {
                await _repository.Get();
            }
            catch (UnableToDoActionException ex)
            {
                Assert.AreEqual("Unable to get the Products", ex.Message);
            }
        }
    }
}
