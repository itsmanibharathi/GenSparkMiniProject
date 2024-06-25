using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Models.Enums;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Service for managing customer orders.
    /// </summary>
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerAddressRepository _customerAddressRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly IOnlinePaymentRepository _onlinePaymentsRepository;
        private readonly ICashPaymentRepository _cashPaymentRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructs a new instance of <see cref="CustomerOrderService"/>.
        /// </summary>
        /// <param name="customerOrderRepository">The repository for customer orders.</param>
        /// <param name="productRepository">The repository for products.</param>
        /// <param name="restaurantRepository">The repository for restaurants.</param>
        /// <param name="onlinePaymentsRepository">The repository for online payments.</param>
        /// <param name="cashPaymentRepository">The repository for cash payments.</param>
        /// <param name="customerAddressRepository">The repository for customer addresses.</param>
        /// <param name="mapper">The AutoMapper instance for mapping DTOs to entities.</param>
        public CustomerOrderService(
            ICustomerOrderRepository customerOrderRepository,
            IProductRepository productRepository,
            IRestaurantRepository restaurantRepository,
            IOnlinePaymentRepository onlinePaymentsRepository,
            ICashPaymentRepository cashPaymentRepository,
            ICustomerAddressRepository customerAddressRepository,
            IMapper mapper)
        {
            _customerAddressRepository = customerAddressRepository;
            _productRepository = productRepository;
            _restaurantRepository = restaurantRepository;
            _customerOrderRepository = customerOrderRepository;
            _onlinePaymentsRepository = onlinePaymentsRepository;
            _cashPaymentRepository = cashPaymentRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new customer order.
        /// </summary>
        /// <param name="createCustomerOrderDto">The DTO containing data for the new customer order.</param>
        /// <returns>A collection of DTOs for the created customer orders.</returns>
        /// <exception cref="EntityNotFoundException{Restaurant}">Thrown when the restaurant is not found.</exception>
        /// <exception cref="EntityNotFoundException{Product}">Thrown when the product is not found.</exception>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer address is not found.</exception>
        /// <exception cref="ProductUnAvailableException">Thrown when a product is not available.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnCustomerOrderDto>> CreateOrder(CustomerOrderDto createCustomerOrderDto)
        {
            try
            {
                var customerAddressCode = _customerAddressRepository.GetAsync(createCustomerOrderDto.ShippingAddressId).Result.Code;

                IEnumerable<(Product product, int qty)> orderProducts = new List<(Product product, int qty)>();

                foreach (var item in createCustomerOrderDto.OrderItemIds)
                {
                    var product = await _productRepository.GetAsync(item.ProductId);
                    if (product == null || !product.ProductAvailable)
                    {
                        throw new ProductUnAvailableException(product?.ProductName ?? "Unknown Product");
                    }
                    orderProducts = orderProducts.Append((product, item.Quantity));
                }

                var orderGroup = orderProducts.GroupBy(x => x.product.RestaurantId).Select(x => new { restaurantId = x.Key, products = x.ToList() });

                var orders = orderGroup
                    .Select(x =>
                    new Order
                    {
                        CustomerId = createCustomerOrderDto.CustomerId,
                        RestaurantId = x.restaurantId,

                        DiscountRat = 0,
                        OrderStatus = OrderStatus.Create,
                        TaxRat = 0.05m,
                        CustomerAddressId = createCustomerOrderDto.ShippingAddressId,
                        ShippingPrice = calShipingPrice(_restaurantRepository.GetAsync(x.restaurantId).Result.AddressCode, customerAddressCode),
                        TotalOrderPrice = x.products.Sum(y => y.product.ProductPrice * y.qty),
                        OrderItems = x.products
                            .Select(y => new OrderItem
                            {
                                ProductId = y.product.ProductId,
                                Quantity = y.qty,
                                Price = y.product.ProductPrice
                            }).ToList(),
                    }).ToList();

                foreach (var order in orders)
                {
                    await _customerOrderRepository.AddAsync(order);
                }
                return _mapper.Map<IEnumerable<ReturnCustomerOrderDto>>(orders);
            }
            catch (EntityNotFoundException<Restaurant>)
            {
                throw;
            }
            catch (EntityNotFoundException<Product>)
            {
                throw;
            }
            catch (EntityNotFoundException<CustomerAddress>)
            {
                throw;
            }
            catch (ProductUnAvailableException)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }

        private int calShipingPrice(AddressCode from, AddressCode to)
        {
            return Math.Abs((int)from - (int)to) * 10;
        }

        /// <summary>
        /// Retrieves all orders for a given customer ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>A collection of DTOs for the retrieved customer orders.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the customer does not have any orders.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnCustomerOrderDto>> GetAllOrders(int customerId)
        {
            try
            {
                var res = await _customerOrderRepository.GetOrdersByCustomerIdAsync(customerId);
                return _mapper.Map<IEnumerable<ReturnCustomerOrderDto>>(res.OrderByDescending(o => o.OrderDate));
            }
            catch (EntityNotFoundException<Order>)
            {
                throw new EntityNotFoundException<Order>($"User {customerId} does not have any orders");
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a specific order for a given customer ID and order ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="orderId">The ID of the order to retrieve.</param>
        /// <returns>The DTO of the retrieved customer order.</returns>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when the order is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the user does not belong to the order.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnCustomerOrderDto> GetOrder(int customerId, int orderId)
        {
            try
            {
                var res = await _customerOrderRepository.GetAsync(orderId);
                if (res == null || res.CustomerId != customerId)
                {
                    throw new UnauthorizedAccessException($"User {customerId} does not belong to this Order {orderId}");
                }
                return _mapper.Map<ReturnCustomerOrderDto>(res);
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates an online payment for one or more customer orders.
        /// </summary>
        /// <param name="orderPaymentDto">The DTO containing data for the payment.</param>
        /// <returns>The DTO of the created online payment.</returns>
        /// <exception cref="UnauthorizedAccessException" >Thrown when the user does not belong to the order.</exception>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when an order is not found.</exception>
        /// <exception cref="InvalidOrderException">Thrown when an order is not valid for payment.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnOrderOnlinePaymentDto> CreateOnlinePayment(CustomerOrderPaymentDto orderPaymentDto)
        {
            try
            {
                var orders = new List<Order>();
                foreach (var o in orderPaymentDto.Orders)
                {
                    var order = await _customerOrderRepository.GetAsync(o);
                    if (order.CustomerId != orderPaymentDto.CustomerId)
                    {
                        throw new UnauthorizedAccessException($"User {orderPaymentDto.CustomerId} does not belong to this Order {order.OrderId}");
                    }
                    if (order.OrderStatus != OrderStatus.Create)
                    {
                        throw new InvalidOrderException($"This Order Id {order.OrderId} is already paid");
                    }
                    order.UpdateAt = DateTime.Now;
                    order.PaymentMethod = orderPaymentDto.PaymentMethod;
                    order.OrderStatus = OrderStatus.Place;
                    orders.Add(order);
                }

                var totalAmount = orders.Sum(x => x.TotalAmount);
                if (totalAmount <= 0 && orderPaymentDto.PaymentMethod != PaymentMethod.Online)
                {
                    throw new InvalidOrderException();
                }

                var onlinePayment = new OnlinePayment
                {
                    CustomerId = orderPaymentDto.CustomerId,
                    PaymentDate = DateTime.Now,
                    PaymentStatus = PaymentStatus.Paid,
                    PaymentRef = Guid.NewGuid().ToString(),
                    PaymentAmount = totalAmount,
                    Orders = orders
                };
                onlinePayment = await _onlinePaymentsRepository.AddAsync(onlinePayment);
                return _mapper.Map<ReturnOrderOnlinePaymentDto>(onlinePayment);
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (InvalidOrderException)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates cash payments for one or more customer orders.
        /// </summary>
        /// <param name="orderPaymentDto">The DTO containing data for the payment.</param>
        /// <returns>A collection of DTOs for the created cash payments.</returns>
        /// <exception cref="UnauthorizedAccessException" >Thrown when the user does not belong to the order.</exception>
        /// <exception cref="EntityNotFoundException{Order}">Thrown when an order is not found.</exception>
        /// <exception cref="InvalidOrderException">Thrown when an order is not valid for payment.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnOrderCashPaymentDto>> CreateCashPayment(CustomerOrderPaymentDto orderPaymentDto)
        {
            try
            {
                List<Order> orders = new List<Order>();
                foreach (var o in orderPaymentDto.Orders)
                {
                    var order = await _customerOrderRepository.GetAsync(o);
                    if (order.CustomerId != orderPaymentDto.CustomerId)
                    {
                        throw new UnauthorizedAccessException($"User {orderPaymentDto.CustomerId} does not belong to this Order {order.OrderId}");
                    }
                    if (order.OrderStatus != OrderStatus.Create)
                    {
                        throw new InvalidOrderException($"This Order Id {order.OrderId} is already paid");
                    }
                    order.UpdateAt = DateTime.Now;
                    order.PaymentMethod = orderPaymentDto.PaymentMethod;
                    order.OrderStatus = OrderStatus.Place;
                    orders.Add(order);
                }

                if (orderPaymentDto.PaymentMethod != PaymentMethod.COD)
                {
                    throw new InvalidOrderException();
                }

                List<CashPayment> cashPayments = new List<CashPayment>();
                foreach (var order in orders)
                {
                    var cashPayment = new CashPayment
                    {
                        PaymentDate = DateTime.Now,
                        PaymentAmount = order.TotalAmount,
                        OrderId = order.OrderId,
                        Order = order
                    };
                    cashPayment = await _cashPaymentRepository.AddAsync(cashPayment);
                    cashPayments.Add(cashPayment);
                }
                return _mapper.Map<IEnumerable<ReturnOrderCashPaymentDto>>(cashPayments);
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (EntityNotFoundException<Order>)
            {
                throw;
            }
            catch (InvalidOrderException)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }
    }
}
