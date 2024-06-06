using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Models.Enums;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Services.CustomerOrderServices
{
    [TestFixture]
    internal class CustomerOrderServiceTest : ServicesTestBase
    {

        [SetUp]
        public async Task Setup()
        {
            await base.Setup();

            CustomerSeedData();
            RestaurantSeedData();

            SetupCustomerOrderRepository();
            SetupProductRepository();
            SetupOnlinePaymentRepository();
            SetupCashPaymentRepository();
            SetupCustomerAddressRepository();
            SetupRestaurantRepository();



            _customerOrderService = new CustomerOrderService(_customerOrderRepository, _productRepository, _restaurantRepository, _onlinePaymentsRepository, _cashPaymentRepository, _customerAddressRepository, _mapper);
        }


        [Test]
        public async Task AddOrder1()
        {
            // Act
            var order = new CustomerOrderDto()
            {
                CustomerId = 1,
                ShippingAddressId = 1,
                OrderItemIds = new CustomerOrderItemDto[]
                {
                    new CustomerOrderItemDto()
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                },
            };
            var response = await _customerOrderService.CreateOrder(order);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() == 1);
            Assert.IsTrue(response.First().OrderStatus == OrderStatus.Create);
        }

        [Test]
        public async Task AddOrder2()
        {
            // Act
            var order = new CustomerOrderDto()
            {
                CustomerId = 1,
                ShippingAddressId = 1,
                OrderItemIds = new CustomerOrderItemDto[]
                {
                    new CustomerOrderItemDto()
                    {
                        ProductId = 1,
                        Quantity = 1
                    },
                    new CustomerOrderItemDto()
                    {
                        ProductId = 3,
                        Quantity = 1
                    }

                },
            };
            var response = await _customerOrderService.CreateOrder(order);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() ==2);
            Assert.IsTrue(response.First().OrderStatus == OrderStatus.Create);
        }

        [Test]
        public async Task AddOrderProductUnAvalable()
        {
            // Act
            var order = new CustomerOrderDto()
            {
                CustomerId = 1,
                ShippingAddressId = 1,
                OrderItemIds = new CustomerOrderItemDto[]
                {
                    new CustomerOrderItemDto()
                    {
                        ProductId = 2,
                        Quantity = 1
                    }
                },
            };

            Assert.ThrowsAsync<ProductUnAvailableException>(async () => await _customerOrderService.CreateOrder(order));
        }

        [Test]
        public async Task AddOrderProductNoFound()
        {
            // Act
            var order = new CustomerOrderDto()
            {
                CustomerId = 1,
                ShippingAddressId = 1,
                OrderItemIds = new CustomerOrderItemDto[]
                {
                    new CustomerOrderItemDto()
                    {
                        ProductId = 0,
                        Quantity = 1
                    }
                },
            };
            Assert.ThrowsAsync<EntityNotFoundException<Product>>(async () => await _customerOrderService.CreateOrder(order));
        }

        [Test]
        public async Task AddOrderCustomerAddressNotfound()
        {
            // Act
            var order = new CustomerOrderDto()
            {
                CustomerId = 1,
                ShippingAddressId = 0,
                OrderItemIds = new CustomerOrderItemDto[]
                {
                    new CustomerOrderItemDto()
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                },
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<AggregateException>(async () =>
                await _customerOrderService.CreateOrder(order)
            );

            Assert.That(ex.InnerException, Is.InstanceOf<EntityNotFoundException<CustomerAddress>>());

        }

        [Test]
        public async Task AddOrderIntenalServerError()
        {
            // Arrange
            DummyDB();
            SetupCustomerOrderRepository();
            SetupProductRepository();
            SetupOnlinePaymentRepository();
            SetupCashPaymentRepository();
            SetupCustomerAddressRepository();
            SetupRestaurantRepository();
            _customerOrderService = new CustomerOrderService(_customerOrderRepository, _productRepository, _restaurantRepository, _onlinePaymentsRepository, _cashPaymentRepository, _customerAddressRepository, _mapper);
            // Act
            var order = new CustomerOrderDto()
            {
                CustomerId = 1,
                ShippingAddressId = 0,
                OrderItemIds = new CustomerOrderItemDto[]
                {
                    new CustomerOrderItemDto()
                    {
                        ProductId = 1,
                        Quantity = 1
                    }
                },
            };
            var ex = Assert.ThrowsAsync<AggregateException>(async () => await _customerOrderService.CreateOrder(order));

            Assert.That(ex.InnerException, Is.InstanceOf<UnableToDoActionException>());
        }
    }
}
