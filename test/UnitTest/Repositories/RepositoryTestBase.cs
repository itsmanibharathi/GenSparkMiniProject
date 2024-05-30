using API.Context;
using API.Models;
using API.Models.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest.Repositories
{
    public class RepositoryTestBase
    {
        public DBGenSparkMinirojectContext _context;

        [SetUp]
        public async Task Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBGenSparkMinirojectContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new DBGenSparkMinirojectContext(optionsBuilder.Options);
            await SeedCustomerData();
        }

        private async Task SeedCustomerData()
        {
            _context.Customers.AddRange(SeedDatas.Customers);
            _context.Restaurants.AddRange(SeedDatas.Restaurants);
                
            await _context.SaveChangesAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await ClearDatabase();
            _context.Dispose();
        }

        private async Task ClearDatabase()
        {
            try
            {
                _context.Customers.RemoveRange(_context.Customers);
                _context.CustomerAuths.RemoveRange(_context.CustomerAuths);
                _context.CustomerAddresses.RemoveRange(_context.CustomerAddresses);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // ignored
            }
        }
        public void DummyDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBGenSparkMinirojectContext>()
                .UseSqlServer("InvalidConnectionString");
            _context = new DBGenSparkMinirojectContext(optionsBuilder.Options);
        }

        public void EmptyDB()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBGenSparkMinirojectContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new DBGenSparkMinirojectContext(optionsBuilder.Options);
        }
    }
}
