using API.Models;
using API.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UnitTest
{
    public static class SeedDatas
    {
        #region Customer
        public static List<Customer> Customers = new List<Customer>
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

        };
        #endregion

        #region Restaurant
        public static List<Restaurant> Restaurants =
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
                    FssaiLicenseNumber = 1,
                    RestaurantAuth = new RestaurantAuth
                    {
                        Password = "abc;xyz"
                    },
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductName = "Chicken Rise",
                            ProductDescription = "Chicken",
                            ProductPrice = 100,
                            ProductCategories = ProductCategory.Food
                        }
                    }
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
                    FssaiLicenseNumber = 12,
                    RestaurantAuth = new RestaurantAuth
                    {
                        Password = "abc;xyz"
                    },
                    Products = new List<Product>
                    {
                        new Product
                        {
                            ProductName = "Pizza",
                            ProductDescription = "Pizza",
                            ProductPrice = 100,
                            ProductCategories = ProductCategory.Food
                        }
                    }
                }
            };
        #endregion

        #region Employee
        public static List<Employee> Employees = new List<Employee>
        {
            new Employee
            {
                EmployeeName = "Joe",
                EmployeeEmail = "joe@gmail.com",
                EmployeePhone = "123456789",
                EmployeeAddress = "123",
                AddressCode = AddressCode.a,
                EmployeeAuth = new EmployeeAuth
                {
                    Password = "abc;xyz"
                }
            }
        };
        #endregion

    }
}
