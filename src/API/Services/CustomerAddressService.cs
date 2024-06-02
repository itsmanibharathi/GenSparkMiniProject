using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using log4net.Util;

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
        public async Task<ReturnCustomerAddressDto> Add(CustomerAddressDto addCustomerAddressDto)
        {
            var res= await _repository.AddAsync(_mapper.Map<CustomerAddress>(addCustomerAddressDto));
            return _mapper.Map<ReturnCustomerAddressDto>(res);
        }

        public async Task<SuccessDto> Delete(int CustomerId, int CustomerAddressId)
        {
            var res = await _repository.DeleteAsync(CustomerId,CustomerAddressId);
            return res ? new SuccessDto("Address Deleted Successfully") : throw new EntityNotFoundException<CustomerAddress>(CustomerAddressId);
        }

        public async Task<IEnumerable<ReturnCustomerAddressDto>> Get(int CustomerId)
        {
            var res = await _repository.GetByCustomerIdAsync(CustomerId);
            return _mapper.Map<IEnumerable<ReturnCustomerAddressDto>>(res);
        }
        public async Task<ReturnCustomerAddressDto> Get(int CustomerId, int CustomerAddressId)
        {
            try
            {
                var res = await _repository.GetAsync(CustomerAddressId);
                if (res.CustomerId != CustomerId)
                {
                    throw new EntityNotFoundException<CustomerAddress>(CustomerAddressId);
                }
                return _mapper.Map<ReturnCustomerAddressDto>(res);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
