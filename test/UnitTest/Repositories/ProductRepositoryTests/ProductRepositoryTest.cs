using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Models.Enums;
using API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.ProductRepositoryTests
{
    [TestFixture]
    internal class ProductRepositoryTest : RepositoryTestBase
    {
        private ProductRepository _productRepository;

        [SetUp]
        public async Task Setup()
        {
            _productRepository = new ProductRepository(_context);
            await RestaurantSeedData();
        }

        [Test]
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

            var result = await _productRepository.AddAsync(product);
            Assert.IsTrue(result.ProductId == 3);
        }

        [Test]
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
                await _productRepository.AddAsync(product);
            }
            catch (EntityAlreadyExistsException<Product> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public async Task AddProductInternalServerException()
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
                await _productRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass(ex.Message);
            }
        }

        [Test]
        public async Task GetProduct()
        {
            var result = await _productRepository.GetAsync(1);
            Assert.IsTrue(result.ProductId == 1);
        }
        [Test]
        public async Task GetProductNotFoundException()
        {
            try
            {
                var result = await _productRepository.GetAsync(3);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
        [Test]
        public async Task GetProductInternalServerException()
        {
            DummyDB();
            try
            {
                var result = await _productRepository.GetAsync(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
        [Test]
        public async Task GetAllProduct()
        {
            var result = await _productRepository.GetAsync();
            Assert.IsTrue(result.Count() == 2);
        }

        [Test]
        public async Task GetAllProductInternalServerException()
        {
            DummyDB();
            try
            {
                var result = await _productRepository.GetAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateProduct()
        {
            var product = await _productRepository.GetAsync(1);
            product.ProductName = "Pizza";
            var result = await _productRepository.UpdateAsync(product);
            Assert.IsTrue(result.ProductName == "Pizza");
        }
        [Test]
        public async Task UpdateProductNotFoundException()
        {
            try
            {
                var product = await _productRepository.GetAsync(3);
                product.ProductName = "Pizza";
                var result = await _productRepository.UpdateAsync(product);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task UpdateProductInternalServerException()
        {
            DummyDB();
            try
            {
                var product = await _productRepository.GetAsync(1);
                product.ProductName = "Pizza";
                var result = await _productRepository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
        [Test]
        public async Task DeleteProduct()
        {
            var result = await _productRepository.DeleteAsync(1);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteProductNotFoundException()
        {
            try
            {
                var result = await _productRepository.DeleteAsync(3);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
        [Test]
        public async Task DeleteProductInternalServerException()
        {
            DummyDB();
            try
            {
                var result = await _productRepository.DeleteAsync(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }

        [Test]
        public async Task SearchProduct()
        {
            var query = new CustomerProductSearchDto()
            {
                ProductName = "Chicken Rise",
                ProductAvailable = false

            };
            var result = await _productRepository.GetSearchAsync(query);
            Assert.IsTrue(result.Count() == 1);
        }

        [Test]
        public async Task SearchProductInternalServerException()
        {
            DummyDB();
            var query = new CustomerProductSearchDto()
            {
                ProductName = "Chicken Rise"
            };
            try
            {
                var result = await _productRepository.GetSearchAsync(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
        [Test]
        public async Task SearchProductNotFoundException()
        {
            var query = new CustomerProductSearchDto()
            {
                ProductName = "Pizzaa"
            };
            try
            {
                var result = await _productRepository.GetSearchAsync(query);
            }
            catch (EntityNotFoundException<Product> ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Pass();
            }
        }
    }
}
