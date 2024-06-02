using API.Context;
using API.Models;
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
    }
}
