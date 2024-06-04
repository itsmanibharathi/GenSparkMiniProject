using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Models.DTOs.EmployeeDto;
using API.Models.DTOs.RestaurantDto;
using AutoMapper;
using Microsoft.Win32;

namespace API.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer Register
            CreateMap<CustomerRegisterDto, Customer>();
            CreateMap<Customer, ReturnCustomerRegisterDto>();

            // Customer Login
            CreateMap<CustomerLoginDto, Customer>();
            CreateMap<Customer, ReturnCustomerLoginDto>();

            // Customer Address
            CreateMap<CustomerAddressDto, CustomerAddress>();
            CreateMap<CustomerAddress, ReturnCustomerAddressDto>();

            // Customer Order Create
            CreateMap<CustomerOrderDto, Order>();
            CreateMap<Order, ReturnCustomerOrderDto>();
            CreateMap<OrderItem, ReturnCustomerOrderItemDto>();

            // Customer Order Payment
            CreateMap<OnlinePayment, ReturnOrderOnlinePaymentDto>();
            CreateMap<CashPayment,ReturnOrderCashPaymentDto>();

            // Restaurant Register
            CreateMap<RestaurantRegisterDto, Restaurant>();
            CreateMap<Restaurant, ReturnRestaurantRegisterDto>();

            
            // Restaurant Login
            CreateMap<RestaurantLoginDto, Restaurant>();
            CreateMap<Restaurant, ReturnRestaurantLoginDto>();

            // Return Search Product
            CreateMap<Product, ReturnCustomerSearchProductDto>();

            // Restaurant Product
            CreateMap<RestaurantProductDto, Product>();
            CreateMap<Product, ReturnRestaurantProductDto>();
            CreateMap<ReturnRestaurantProductDto, RestaurantProductDto>();

            // Restaurant Order
            CreateMap<Order, ReturnRestaurantOrderDto>();
            // Employee Register
            CreateMap<EmployeeRegisterDto, Employee>();
            CreateMap<Employee, ReturnEmployeeRegisterDto>();

            // Employee Login
            CreateMap<EmployeeLoginDto, Employee>();
            CreateMap<Employee, ReturnEmployeeLoginDto>();

            // Employee Order
            CreateMap<Order, ReturnEmployeeOrderDto>();
        }
    }
}
