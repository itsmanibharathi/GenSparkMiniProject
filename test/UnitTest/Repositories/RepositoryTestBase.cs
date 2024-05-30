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
            _context.Customers.AddRange(
                new List<Customer>
                {
                    new Customer
                    {
                        CustomerName = "Mani", CustomerEmail = "mani@gmail.com", CustomerPhone = "123456789",
                        CustomerAuth = new CustomerAuth
                        {
                            Password = "abc;xyz"
                        },
                        Addresses = new List<CustomerAddress>
                        {
                            new CustomerAddress
                            {
                                Type = AddressType.Home,
                                Code = AddressCode.a,
                                City = "xxx",
                                State = "yyy"
                            }
                        }
                    },
                    new Customer
                    {
                        CustomerName = "Kiko", CustomerEmail = "kiko@gmail.com", CustomerPhone = "987654321",
                        CustomerAuth = new CustomerAuth
                        {
                            Password = "abc;xyz"
                        },
                        Addresses = new List<CustomerAddress>
                        {
                            new CustomerAddress
                            {
                                Type = AddressType.Work,
                                Code = AddressCode.z,
                                City = "xxx",
                                State = "yyy"
                            }
                        }
                    }
                });
            _context.Restaurants.AddRange(
                new List<Restaurant>
                {
                    new Restaurant()
                    {
                        Name = "KFC",
                        Description = "Fast Food",
                        Phone = "123456789",
                        Email = "kfc@gmail.com",
                        Branch = "Erode Main",
                        Address = "Erode",
                        City = "Erode",
                        State = "Tamil Nadu",
                        Zip = "638001",
                        AddressCode = AddressCode.d,
                        FssaiLicenseNumber = 1
                    },
                    new Restaurant()
                    {
                        Name = "Dominos",
                        Description = "Fast Food",
                        Phone = "123456789",
                        Email = "dominos@gmail.com",
                        Branch = "Erode Main",
                        Address = "Erode",
                        City = "Erode",
                        State = "Tamil Nadu",
                        Zip = "638001",
                        AddressCode = AddressCode.m,
                        FssaiLicenseNumber = 12
                    }
                });
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
