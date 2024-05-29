using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.Enums;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;

namespace API.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly IRepository<int, Product> _productRepository;
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly ICustomerAddressRepository _customerAddressRepository;
        private readonly IRepository<int, Restaurant> _restaurantRepository;
        private readonly IMapper _mapper;
            
        public CustomerOrderService(
            ICustomerOrderRepository customerOrderRepository,
            IRepository<int,Product> productrepository,
            IRepository<int,Customer> customerRepository,
            IRepository<int,Restaurant> restaurantRepository,
            ICustomerAddressRepository customerAddressRepository,
            IMapper mapper)
        {
            _customerOrderRepository = customerOrderRepository;
            _productRepository = productrepository;
            _customerRepository = customerRepository;
            _customerAddressRepository = customerAddressRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReturnCreateCustomerOrderDto>> CreateOrder(CreateCustomerOrderDto createCustomerOrderDto)
        {
            var customer = await _customerRepository.Get(createCustomerOrderDto.CustomerId);

            var customerAddressCode = _customerAddressRepository.Get(createCustomerOrderDto.CustomerId, createCustomerOrderDto.ShippingAddressId).Result.Code;

            //var orderProducts = await Task.WhenAll(createCustomerOrderDto.OrderItemIds.Select(async x => new
            //{
            //    product = await _productRepository.Get(x.ProductId),
            //    qty = x.Quantity
            //}));

            var orderProducts = await Task.WhenAll(createCustomerOrderDto.OrderItemIds.Select(async x =>
            {
                var product = await _productRepository.Get(x.ProductId);
                if (product == null || !product.ProductAvailable)
                {
                    throw new ProductUnAvailableException(product.ProductName );
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
                new Order { 
                    CustomerId = customer.CustomerId, 
                    RestaurantId = x.restaurantId,
                    DiscountRat =0,
                    TaxRat = 0.05m,
                    CustomerAddressId = createCustomerOrderDto.ShippingAddressId,
                    ShippingPrice = calShipingPrice(_restaurantRepository.Get(x.restaurantId).Result.AddressCode , customerAddressCode  ),
                    TotalOrderPrice = x.products.Sum(y => y.product.ProductPrice * y.qty),
                    OrderItems = x.products
                        .Select(y => new OrderItem { 
                            ProductId = y.product.ProductId, 
                            Quantity = y.qty,
                            Price = y.product.ProductPrice
                        }).ToList(),
                }).ToList();

            foreach (var order in orders)
            {
                await _customerOrderRepository.Add(order);
            }

            return _mapper.Map<IEnumerable<ReturnCreateCustomerOrderDto>>(orders);
        }
        int calShipingPrice(AddressCode from, AddressCode to)
        {
            return Math.Abs(from - to) * 10;
        }

        public async Task<IEnumerable<ReturnCreateCustomerOrderDto>> GetAllOrders(int customerId)
        {
            var res= await _customerOrderRepository.GetAll(customerId);
            return _mapper.Map<IEnumerable<ReturnCreateCustomerOrderDto>>(res);
        }

        public async Task<ReturnCreateCustomerOrderDto> GetOrder(int customerId, int orderId)
        {
            var res = await _customerOrderRepository.Get(customerId, orderId);
            return _mapper.Map<ReturnCreateCustomerOrderDto>(res);
        }
    }
}
