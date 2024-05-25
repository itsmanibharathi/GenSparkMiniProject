using API.Models;
using API.Models.DTOs;
using API.Repositories;
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

        }
    }
}
