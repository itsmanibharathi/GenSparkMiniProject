using API.Models.DTOs.CustomerDto;

namespace API.Services.Interfaces
{
    public interface IProductSerivce
    {
        public Task<IEnumerable<ReturnCustomerSearchProductDto>> Search(CustomerProductSearchDto productSearchDto);
        public Task<IEnumerable<ReturnCustomerSearchProductDto>> Get();
        public Task<ReturnCustomerSearchProductDto> Get(int id);
    }
}
