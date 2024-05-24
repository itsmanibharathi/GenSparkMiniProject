using API.Context;
using API.Exceptions;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace API.Repositories
{
    public class CustomerRepository : IRepository<int, Customer>
    {
        protected readonly DBGenSparkMinirojectContext _context;

        public CustomerRepository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }
        public virtual async Task<Customer> Add(Customer entity)
        {
            try
            {
                if(!await IsDuplicate(entity))
                {
                    _context.Customers.Add(entity);
                    var res= await _context.SaveChangesAsync();
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
                throw new UnableToDoActionException("Unable to Insert the new Customer",ex);
            }
        }

        private async Task<bool> IsDuplicate(Customer entity)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerEmail == entity.CustomerEmail && c.CustomerPhone == entity.CustomerPhone);
            return customer != null;
        }

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Customer> Get(int id)
        {
            try
            {

                return (await _context.Customers.FirstOrDefaultAsync(c=> c.CustomerId == id))?? throw new NoEmployeeInThisIdException();
            }
            catch (NoEmployeeInThisIdException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Customer",ex);
            }
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            try
            {
                return await _context.Customers.ToListAsync() ?? throw new EmptyDatabaseException("Customer DB Empty");
            }
            catch (EmptyDatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Customers",ex);
            }
        }

        public virtual async Task<Customer> Update(Customer entity)
        {
            try
            {
                _context.Customers.Update(entity);
                var res = await _context.SaveChangesAsync();
                return res > 0 ? entity : throw new UnableToDoActionException("Unable to update");
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to update the Customer",ex);
            }
        }
    }
}
