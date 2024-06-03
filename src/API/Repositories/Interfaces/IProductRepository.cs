using API.Models;
using API.Models.DTOs.CustomerDto;

namespace API.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<int, Product>
    {
        public Task<IEnumerable<Product>> GetSearchAsync(CustomerProductSearchDto productSearchDto);
    }
}
