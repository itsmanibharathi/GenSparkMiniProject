using API.Models;
using API.Models.DTOs;
using API.Models.Enums;
using API.Repositories;
using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Repositories.ProductRepositoryTest
{
    [TestFixture]
    internal class ProductSearchRepositoryTest : RepositoryTestBase
    {
        private IProductSearchRepository _repository;

        [SetUp]
        public async Task Setup()
        {
            _repository = new ProductRepository(_context);
        }
        [Ignore("Ignore Test")]
        // Search Product
        [Test, Order(1)]
        public async Task SearchProduct()
        {
            ProductSearchDto productSearchDto = new ProductSearchDto()
            {
                ProductPrice = 100
            };
            var result = await _repository.Search(productSearchDto);
            Console.WriteLine(result.Count());
            Assert.IsTrue(result.Count() == 1);
        }
    }
}
