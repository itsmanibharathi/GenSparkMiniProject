using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;

namespace API.Services.Interfaces
{
    public interface ICustomerAddressService
    {
        public Task<ReturnCustomerAddressDto> Add(CustomerAddressDto addCustomerAddressDto);
        public Task<IEnumerable<ReturnCustomerAddressDto>> Get(int CustomerId);
        public Task<ReturnCustomerAddressDto> Get(int CustomerId, int CustomerAddressId);
        //Task<SuccessDto> Update(UpdateCustomerAddressDto updateCustomerAddressDto);
        public Task<SuccessDto> Delete(int CustomerId, int CustomerAddressId);

    }
}
