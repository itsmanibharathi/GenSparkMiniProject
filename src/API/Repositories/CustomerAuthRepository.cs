using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace API.Repositories
{
    public class CustomerAuthRepository : ICustomerAuthRepository
    {
        private readonly DBGenSparkMinirojectContext _context;

        [ExcludeFromCodeCoverage]
        public CustomerAuthRepository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }
        public async Task<Customer> Get(string email)
        {
            try
            {
                return (await _context.Customers
                    .Include(c => c.CustomerAuth)
                    .FirstOrDefaultAsync(c => c.CustomerEmail == email)
                    ?? throw new InvalidUserCredentialException());   
            }
            catch (InvalidUserCredentialException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Login", ex);
            }
        }
    }
}
