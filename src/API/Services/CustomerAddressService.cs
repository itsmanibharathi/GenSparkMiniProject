using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace API.Services
{
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly ICustomerAddressRepository _repository;
        private readonly IMapper _mapper;

        public CustomerAddressService(ICustomerAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ReturnCustomerAddressDto> Add(AddCustomerAddressDto addCustomerAddressDto)
        {
            var res= await _repository.Add(_mapper.Map<CustomerAddress>(addCustomerAddressDto));
            return _mapper.Map<ReturnCustomerAddressDto>(res);
        }

        public async Task<SuccessDto> Delete(int CustomerId, int CustomerAddressId)
        {
            var res = await _repository.Delete(CustomerId , CustomerAddressId);
            if (!res)
            {
                throw new UnableToDoActionException("Unable to Delete the Address");
            }
            return _mapper.Map<SuccessDto>(res);
        }

        public async Task<IEnumerable<ReturnCustomerAddressDto>> Get(int CustomerId)
        {
            var res = await _repository.Get(CustomerId);
            return _mapper.Map<IEnumerable<ReturnCustomerAddressDto>>(res);
        }

        public async Task<ReturnCustomerAddressDto> Get(int CustomerId, int CustomerAddressId)
        {
            var res = await _repository.Get(CustomerId, CustomerAddressId);
            return _mapper.Map<ReturnCustomerAddressDto>(res);
        }
    }
}
