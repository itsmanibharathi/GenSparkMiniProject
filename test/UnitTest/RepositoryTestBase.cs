using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryTest
{
    public class RepositoryTestBase
    {
        public DBGenSparkMinirojectContext _context;

        [SetUp]
        public async Task Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                            .UseInMemoryDatabase("DBGenSparkMiniroject");
            _context = new DBGenSparkMinirojectContext(optionsBuilder.Options);
            await SeedCustomerData();
        }

        private async Task SeedCustomerData()
        {
            _context.Customers.AddRange(
                new List<Customer>
                {
                    new Customer { CustomerName = "Mani",CustomerEmail = "mani@gmail.com", CustomerPhone = "123456789" },
                    new Customer { CustomerName = "Kiko",CustomerEmail = "kiko@gmail.com", CustomerPhone = "987654321"}
                });
            await  _context.SaveChangesAsync();
        }
        [TearDown]
        public async Task TearDown()
        {
            await _context.Database.EnsureDeletedAsync();
            _context.Dispose();
        }
    }
}