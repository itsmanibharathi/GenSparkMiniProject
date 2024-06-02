using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Models.Enums;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerAddressRepository _customerAddressRepository;
        private readonly IProductRepository _productRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly IOnlinePaymentRepository _onlinePaymentsRepository;
        private readonly ICashPaymentRepository _cashPaymentRepository;
        private readonly IMapper _mapper;

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


        public async Task<IEnumerable<ReturnCustomerOrderDto>> CreateOrder(CustomerOrderDto createCustomerOrderDto)
        {
            try
            {


                var customerAddressCode = _customerAddressRepository.GetAsync(createCustomerOrderDto.ShippingAddressId).Result.Code;

                //var orderProducts = await Task.WhenAll(createCustomerOrderDto.OrderItemIds.Select(async x => new
                //{
                //    product = await _productRepository.Get(x.ProductId),
                //    qty = x.Quantity
                //}));

                var orderProducts = await Task.WhenAll(createCustomerOrderDto.OrderItemIds.Select(async x =>
                {
                    var product = await _productRepository.GetAsync(x.ProductId);
                    if (product == null || !product.ProductAvailable)
                    {
                        throw new ProductUnAvailableException(product.ProductName);
                    }
                    return new
                    {
                        product,
                        qty = x.Quantity
                    };
                }));

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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        int calShipingPrice(AddressCode from, AddressCode to)
        {
            return Math.Abs(from - to) * 10;
        }

        public async Task<IEnumerable<ReturnCustomerOrderDto>> GetAllOrders(int customerId)
        {
            var res = await _customerOrderRepository.GetOrdersByCustomerId(customerId);
            return _mapper.Map<IEnumerable<ReturnCustomerOrderDto>>(res.OrderByDescending(o => o.OrderDate));
        }
        public async Task<ReturnCustomerOrderDto> GetOrder(int customerId, int orderId)
        {
            var res = await _customerOrderRepository.GetAsync(orderId);
            if (res == null || res.CustomerId != customerId)
            {
                throw new EntityNotFoundException<Order>(orderId);
            }
            return _mapper.Map<ReturnCustomerOrderDto>(res);
        }

        public async Task<ReturnOrderOnlinePaymentDto> CreateOnlinePayment(CustomerOrderPaymentDto orderPaymentDto)
        {
            try
            {
                var orders = await Task.WhenAll(orderPaymentDto.Orders
                    .Select(async o =>
                    {
                        var order = await _customerOrderRepository.GetAsync(o);
                        if (order.OrderStatus != OrderStatus.Create)
                        {
                            throw new InvalidOrderException(order.OrderId, order.OrderStatus.ToString());
                        }
                        order.UpdateAt = DateTime.Now;
                        order.PaymentMethod = orderPaymentDto.PaymentMethod;
                        order.OrderStatus = OrderStatus.Place;
                        return order;
                    }));

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ReturnOrderCashPaymentDto>> CreateCashPayment(CustomerOrderPaymentDto orderPaymentDto)
        {
            try
            {
                var orders = await Task.WhenAll(orderPaymentDto.Orders
                    .Select(async o =>
                    {
                        var order = await _customerOrderRepository.GetAsync(o);
                        if (order.OrderStatus != OrderStatus.Create)
                        {
                            throw new InvalidOrderException(order.OrderId, order.OrderStatus.ToString());
                        }
                        order.UpdateAt = DateTime.Now;
                        order.PaymentMethod = orderPaymentDto.PaymentMethod;
                        order.OrderStatus = OrderStatus.Place;
                        return order;
                    }));

                if (orderPaymentDto.PaymentMethod != PaymentMethod.COD)
                {
                    throw new InvalidOrderException();
                }
                List<CashPayment> cashPayments = new List<CashPayment>();
                foreach (var order in orders) { 
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
