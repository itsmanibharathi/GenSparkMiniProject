using API.Context;
using API.Exceptions;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CustomerAuthRepository : CustomerRepository
    {
        public CustomerAuthRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        public async override Task<Customer> Add(Customer entity)
        {
            try
            {
                if (!await IsDuplicate(entity))
                {
                    _context.Customers.Add(entity);
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
                throw new UnableToDoActionException("Unable to Insert the new Customer", ex);
            }
        }

        private async Task<bool> IsDuplicate(Customer entity)
        {
            var customer = await _context.Customers
                .Include(c => c.CustomerAuth)
                .FirstOrDefaultAsync(c => c.CustomerAuth.CustomerId == entity.CustomerId);
            return customer != null;
        }

        public async override Task<Customer> Get(int id)
        {
            try
            {
                return (await _context.Customers.Include(c => c.CustomerAuth).FirstOrDefaultAsync(c => c.CustomerId == id)) ?? throw new NoEmployeeInThisIdException();
            }
            catch (NoEmployeeInThisIdException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Get the Customer", ex);
            }
        }
    }
}
