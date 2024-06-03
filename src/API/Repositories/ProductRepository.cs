using API.Context;
using API.Exceptions;
using API.Models;
using API.Models.DTOs.CustomerDto;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductRepository : Repository<int, Product>, IProductRepository
    {
        public ProductRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }
        public override Task<bool> IsDuplicate(Product entity)
        {
            return _context.Products.AnyAsync(x =>x.RestaurantId == entity.RestaurantId && x.ProductCategories == entity.ProductCategories &&  x.ProductName == entity.ProductName);
        }
        public async Task<IEnumerable<Product>> GetSearchAsync(CustomerProductSearchDto productSearchDto)
        {
            try
            {
                var query = _context.Products.Include(p => p.Restaurant).AsQueryable();

                query = query.Where(p => p.ProductAvailable == productSearchDto.ProductAvailable);
                if (!string.IsNullOrEmpty(productSearchDto.ProductName))
                    query = query.Where(p => p.ProductName.Contains(productSearchDto.ProductName));
                if (productSearchDto.ProductPrice.HasValue)
                    query = query.Where(p => p.ProductPrice == productSearchDto.ProductPrice);
                if (productSearchDto.ProductCategories != null)
                    query = query.Where(p => productSearchDto.ProductCategories.Contains(p.ProductCategories));
                if (!string.IsNullOrEmpty(productSearchDto.RestaurantName))
                    query = query.Where(p => p.Restaurant.Name.Contains(productSearchDto.RestaurantName));
                if (!string.IsNullOrEmpty(productSearchDto.RestaurantBranch))
                    query = query.Where(p => p.Restaurant.Branch.Contains(productSearchDto.RestaurantBranch));
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to search products", ex);
            }
        }
    }
}
