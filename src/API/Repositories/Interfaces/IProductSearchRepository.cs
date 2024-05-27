using API.Models;
using API.Models.DTOs;

namespace API.Repositories.Interfaces
{
    public interface IProductSearchRepository
    {
        public Task<IEnumerable<Product>> Search(ProductSearchDto productSearchDto);

    }
}
