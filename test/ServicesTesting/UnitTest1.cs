using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace ServicesTesting
{
    public class Tests
    {
        private DBGenSparkMinirojectContext context;

        [SetUp]
        public void Setup()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                            .UseInMemoryDatabase("dummyDB");
            context = new DBGenSparkMinirojectContext(optionsBuilder.Options);

        }

        [Test]
        public void Get()
        {
            var customer = new CustomerAuth()
            {
                PasswordHash = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };
            context.CustomerAuths.Add(customer);
            context.SaveChanges();

            var res = context.CustomerAuths.ToList();
            Assert.AreEqual(1, res.Count());
        }
    }
}