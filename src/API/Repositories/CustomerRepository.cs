using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

namespace API.Repositories
{
    public class CustomerRepository : Repository<int,Customer>, ICustomerRepository
    {
        public CustomerRepository(DBGenSparkMinirojectContext context) : base(context)
        {

        }

        public async Task<Customer> GetByEmailId(string emailId)
        {
            try
            {
                return await _context.Customers.Include(c => c.CustomerAuth).FirstOrDefaultAsync(c => c.CustomerEmail == emailId) ?? throw new EntityNotFoundException<Customer>(emailId);
            }
            catch (EntityNotFoundException<Customer>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Customer by Email Id", ex);
            }
        }

        public override Task<bool> IsDuplicate(Customer entity)
        {
            return _context.Customers.AnyAsync(c => c.CustomerEmail == entity.CustomerEmail && c.CustomerPhone == entity.CustomerPhone);
        }
    }
}
