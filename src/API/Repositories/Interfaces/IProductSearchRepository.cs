using API.Models;
using API.Models.DTOs.CustomerDto;

namespace API.Repositories.Interfaces
{
    public interface IProductSearchRepository
    {
        public Task<IEnumerable<Product>> Search(CustomerSearchProductDto productSearchDto);

    }
}
