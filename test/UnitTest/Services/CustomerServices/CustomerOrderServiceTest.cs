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

        [Test]

        public async Task PlaceOrderWithCOD1()
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

            var orderPlaced = new CustomerOrderPaymentDto()
            {
                CustomerId = 1,
                Orders = response.Select(x => x.OrderId).ToList(),
                PaymentMethod = PaymentMethod.COD
            };

            var paymentResponse = await _customerOrderService.CreateCashPayment(orderPlaced);

            Assert.IsNotNull(paymentResponse);
            Assert.IsTrue(paymentResponse.Count() == 1);
            Assert.IsTrue(paymentResponse.First().Order.PaymentStatus == PaymentStatus.Pending);
            Assert.IsTrue(paymentResponse.First().Order.OrderStatus == OrderStatus.Place);
        }

        [Test]

        public async Task PlaceOrderWithCOD2()
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

            var orderPlaced = new CustomerOrderPaymentDto()
            {
                CustomerId = 1,
                Orders = response.Select(x => x.OrderId).ToList(),
                PaymentMethod = PaymentMethod.COD
            };

            var paymentResponse = await _customerOrderService.CreateCashPayment(orderPlaced);

            Assert.IsNotNull(paymentResponse);
            Assert.IsTrue(paymentResponse.Count() == 2);
            Assert.IsTrue(paymentResponse.First().Order.PaymentStatus == PaymentStatus.Pending);
            Assert.IsTrue(paymentResponse.First().Order.OrderStatus == OrderStatus.Place);
        }

        [Test]
        public async Task PlaseOrderWith_CashOrderNotFound()
        {
            var orderPlaced = new CustomerOrderPaymentDto()
            {
                CustomerId = 1,
                Orders = new List<int> { 1, 2 },
                PaymentMethod = PaymentMethod.COD
            };

            Assert.ThrowsAsync<EntityNotFoundException<Order>>(async ()=> await _customerOrderService.CreateCashPayment(orderPlaced));
        }


        [Test]
        public async Task PlaseOrderWith_UnableToDoAction()
        {
            DummyDB();
            SetupCustomerOrderRepository();
            SetupProductRepository();
            SetupOnlinePaymentRepository();
            SetupCashPaymentRepository();
            SetupCustomerAddressRepository();
            SetupRestaurantRepository();
            _customerOrderService = new CustomerOrderService(_customerOrderRepository, _productRepository, _restaurantRepository, _onlinePaymentsRepository, _cashPaymentRepository, _customerAddressRepository, _mapper);
            var orderPlaced = new CustomerOrderPaymentDto()
            {
                CustomerId = 1,
                Orders = new List<int> { 1, 2 },
                PaymentMethod = PaymentMethod.COD
            };

            Assert.ThrowsAsync<UnableToDoActionException>(async () => await _customerOrderService.CreateCashPayment(orderPlaced));
        }


        [Test]
        public async Task PlaceOrderWithOnlinePayment1()
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

            var orderPlaced = new CustomerOrderPaymentDto()
            {
                CustomerId = 1,
                Orders = response.Select(x => x.OrderId).ToList(),
                PaymentMethod = PaymentMethod.Online
            };

            var paymentResponse = await _customerOrderService.CreateOnlinePayment(orderPlaced);

            Assert.IsNotNull(paymentResponse);
            Assert.IsTrue(paymentResponse.PaymentRef!= null);
            Assert.IsTrue(paymentResponse.PaymentStatus == PaymentStatus.Paid);
            Assert.IsTrue(paymentResponse.Orders.First().PaymentMethod == PaymentMethod.Online);
            Assert.IsTrue(paymentResponse.Orders.First().OrderStatus == OrderStatus.Place);
        }

        [Test]
        public async Task PlaceOrderWithOnlinePayment2()
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

            var orderPlaced = new CustomerOrderPaymentDto()
            {
                CustomerId = 1,
                Orders = response.Select(x => x.OrderId).ToList(),
                PaymentMethod = PaymentMethod.Online
            };

            var paymentResponse = await _customerOrderService.CreateOnlinePayment(orderPlaced);

            Assert.IsNotNull(paymentResponse);
            Assert.IsTrue(paymentResponse.PaymentRef != null);
            Assert.IsTrue(paymentResponse.PaymentStatus == PaymentStatus.Paid);
            Assert.IsTrue(paymentResponse.Orders.First().PaymentMethod == PaymentMethod.Online);
            Assert.IsTrue(paymentResponse.Orders.First().OrderStatus == OrderStatus.Place);
        }


        [Test]
        public async Task PlaceOrderWithOnlinePaymentOrderNotFound()
        {
            var orderPlaced = new CustomerOrderPaymentDto()
            {
                CustomerId = 1,
                Orders = new List<int> { 1, 2 },
                PaymentMethod = PaymentMethod.Online
            };

            Assert.ThrowsAsync<EntityNotFoundException<Order>>(async () => await _customerOrderService.CreateOnlinePayment(orderPlaced));
        }

        [Test]
        public async Task PlaceOrderWithOnlinePaymentUnableToDoAction()
        {
            DummyDB();
            SetupCustomerOrderRepository();
            SetupProductRepository();
            SetupOnlinePaymentRepository();
            SetupCashPaymentRepository();
            SetupCustomerAddressRepository();
            SetupRestaurantRepository();
            _customerOrderService = new CustomerOrderService(_customerOrderRepository, _productRepository, _restaurantRepository, _onlinePaymentsRepository, _cashPaymentRepository, _customerAddressRepository, _mapper);
            var orderPlaced = new CustomerOrderPaymentDto()
            {
                CustomerId = 1,
                Orders = new List<int> { 1, 2 },
                PaymentMethod = PaymentMethod.Online
            };

            Assert.ThrowsAsync<UnableToDoActionException>(async () => await _customerOrderService.CreateOnlinePayment(orderPlaced));
        }

        [Test]
        public async Task GetOrders()
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

            var orders = await _customerOrderService.GetAllOrders(1);

            Assert.IsNotNull(orders);
            Assert.IsTrue(orders.Count() == 1);
            Assert.IsTrue(orders.First().OrderStatus == OrderStatus.Create);
        }

        [Test]
        public async Task GetOrdersNotFound()
        {
            Assert.ThrowsAsync<EntityNotFoundException<Order>>(async () => await _customerOrderService.GetAllOrders(0));
        }
    }
}
