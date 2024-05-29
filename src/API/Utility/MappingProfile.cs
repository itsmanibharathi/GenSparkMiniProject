using API.Models;
using API.Models.DTOs;
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
            CreateMap<AddCustomerAddressDto, CustomerAddress>();
            CreateMap<CustomerAddress, ReturnCustomerAddressDto>();

            // Customer Order Create
            CreateMap<CreateCustomerOrderDto, Order>();
            CreateMap<Order, ReturnCreateCustomerOrderDto>();

            // Restaurant Register
            CreateMap<RestaurantRegisterDto, Restaurant>();
            CreateMap<Restaurant, ReturnRestaurantRegisterDto>();

            // Restaurant Login
            CreateMap<RestaurantLoginDto, Restaurant>();
            CreateMap<Restaurant, ReturnRestaurantLoginDto>();

            // Return Search Product
            CreateMap<Product, ReturnSearchProductDto>();

            // Restaurant Product
            CreateMap<RestaurantProductDto, Product>();
            CreateMap<Product, ReturnRestaurantProductDto>();
            CreateMap<ReturnRestaurantProductDto, Product>();

            // Employee Register
            CreateMap<EmployeeRegisterDto, Employee>();
            CreateMap<Employee, ReturnEmployeeRegisterDto>();

            // Employee Login
            CreateMap<EmployeeLoginDto, Employee>();
            CreateMap<Employee, ReturnEmployeeLoginDto>();
        }
    }
}
