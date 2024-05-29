using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeAuthRepository : IEmployeeAuthRepository
    {
        private readonly DBGenSparkMinirojectContext _context;

        public EmployeeAuthRepository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }

        public async Task<Employee> Get(string email)
        {
            try
            {
                return await _context.Employees
                    .Include(e => e.EmployeeAuth)
                    .FirstOrDefaultAsync(e => e.EmployeeEmail == email)
                    ?? throw new InvalidUserCredentialException();
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
