using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface IProductSerivce
    {
        public Task<IEnumerable<ReturnSearchProductDto>> Search(ProductSearchDto productSearchDto);
        public Task<IEnumerable<ReturnSearchProductDto>> Get();
        public Task<ReturnSearchProductDto> Get(int id);
    }
}
