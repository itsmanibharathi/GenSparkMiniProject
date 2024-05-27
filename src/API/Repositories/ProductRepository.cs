using API.Context;
using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductRepository : IRepository<int, Product> , IProductSearchRepository
    {
        protected readonly DBGenSparkMinirojectContext _context;

        public ProductRepository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }
        public virtual async Task<Product> Add(Product entity)
        {
            try
            {
                if (!await IsDuplicate(entity))
                {
                    _context.Products.Add(entity);
                    var res = await _context.SaveChangesAsync();
                    return res > 0 ? entity : throw new UnableToDoActionException("Unable to insert");
                }
                throw new DataDuplicateException();
            }
            catch (DataDuplicateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Insert the new Product", ex);
            }
        }

        private async Task<bool> IsDuplicate(Product entity)
        {
            var Product = await _context.Products.FirstOrDefaultAsync(p => p.RestaurantId == entity.RestaurantId && p.ProductName == entity.ProductName && p.ProductCategories == entity.ProductCategories);
            return Product != null;
        }

        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                _context.Products.Remove(await Get(id));
               var res = await _context.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to delete the Product", ex);
            }
        }

        public virtual async Task<Product> Get(int id)
        {
            try
            {

                return (await _context.Products.Include(p => p.Restaurant).FirstOrDefaultAsync(c => c.ProductId == id)) ?? throw new ProductNotFoundException();
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Product", ex);
            }
        }

        public virtual async Task<IEnumerable<Product>> Get()
        {
            try
            {
                var res = await _context.Products.Include(p => p.Restaurant).ToListAsync();
                return res.Count > 0 ? res : throw new EmptyDatabaseException("Product DB Empty");
            }
            catch (EmptyDatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Products", ex);
            }
        }

        public virtual async Task<Product> Update(Product entity)
        {

            try
            {
                //if (await IsDuplicate(entity))
                //{
                //    throw new DataDuplicateException();
                //}
                var existingEntity = _context.Products.Local.FirstOrDefault(p => p.ProductId == entity.ProductId);

                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }

                // Attach the updated product and set its state to Modified
                _context.Products.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                var res = await _context.SaveChangesAsync();
                return res > 0 ? entity : throw new UnableToDoActionException("Unable to update");
            }
            catch (DataDuplicateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to update the Product", ex);
            }
        }

        public async Task<IEnumerable<Product>> Search(ProductSearchDto productSearchDto)
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
                throw new UnableToDoActionException("Unable to search the Products", ex);
            }
        }
    }
}