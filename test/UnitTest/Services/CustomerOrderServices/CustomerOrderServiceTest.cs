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
        private ICustomerOrderRepository _customerOrderRepository;
        private IProductRepository _productRepository;
        private IRestaurantRepository _restaurantRepository;
        private IOnlinePaymentRepository _onlinePaymentsRepository;
        private ICashPaymentRepository _cashPaymentRepository;
        private ICustomerAddressRepository _customerAddressRepository;
        private ICustomerOrderService _customerOrderService;

        [SetUp]
        public async Task Setup()
        {
            await base.Setup();

            _customerOrderRepository = new CustomerOrderRepository(_context);
            _productRepository = new ProductRepository(_context);
            _restaurantRepository = new RestaurantRepository(_context);
            _onlinePaymentsRepository = new OnlinePaymentRepository(_context);
            _cashPaymentRepository = new CashPaymentRepository(_context);
            _customerAddressRepository = new CustomerAddressRespository(_context);
            _customerOrderService = new CustomerOrderService(_customerOrderRepository, _productRepository, _restaurantRepository, _onlinePaymentsRepository, _cashPaymentRepository, _customerAddressRepository, _mapper);
        }

    }
}
