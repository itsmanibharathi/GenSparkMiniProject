using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface ICustomerAddressService
    {
        public Task<ReturnCustomerAddressDto> Add(AddCustomerAddressDto addCustomerAddressDto);
        public Task<IEnumerable<ReturnCustomerAddressDto>> Get(int CustomerId);
        public Task<ReturnCustomerAddressDto> Get(int CustomerId, int CustomerAddressId);
        //Task<SuccessDto> Update(UpdateCustomerAddressDto updateCustomerAddressDto);
        public Task<SuccessDto> Delete(int id);

    }
}
