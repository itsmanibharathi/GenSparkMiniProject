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

            // Customer Get
            CreateMap<Customer, ReturnCustomerDto>();

            // Customer Order Create
            CreateMap<CustomerOrderDto, Order>();
            CreateMap<Order, ReturnCustomerOrderDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.EmployeeName));
            // customer mapping for order item
            CreateMap<OrderItem, ReturnCustomerOrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Product.Restaurant.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.ProductPrice))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.ProductDescription));

            //ReturnCustomerOrderAddressDto
            CreateMap<CustomerAddress, ReturnCustomerOrderAddressDto>();

            // Customer Order Payment
            CreateMap<OnlinePayment, ReturnOrderOnlinePaymentDto>();
            CreateMap<CashPayment, ReturnOrderCashPaymentDto>();

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
            CreateMap<RestaurantProductDto, Product>();
            CreateMap<Product, ReturnRestaurantProductDto>();
            CreateMap<ReturnRestaurantProductDto, RestaurantProductDto>();


            // Restaurant Order
            CreateMap<Order, ReturnRestaurantOrderDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.EmployeeName));


            CreateMap<OrderItem, ReturnRestaurantOrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.RestaurantName, opt=> opt.MapFrom(src => src.Product.Restaurant.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.ProductPrice))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.ProductDescription));

            // Employee Register
            CreateMap<EmployeeRegisterDto, Employee>();
            CreateMap<Employee, ReturnEmployeeRegisterDto>();

            // Employee Login
            CreateMap<EmployeeLoginDto, Employee>();
            CreateMap<Employee, ReturnEmployeeLoginDto>();

            // Employee Order
            CreateMap<Order, ReturnEmployeeOrderDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.EmployeeName));

            CreateMap<OrderItem, ReturnEmployeeOrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Product.Restaurant.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.ProductPrice))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.ProductDescription));
        }
    }
}
