using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace API.Repositories
{
    public class EmployeeRepository : Repository<int,Employee> , IEmployeeRepository
    {
        public EmployeeRepository(DBGenSparkMinirojectContext context) : base(context)
        {
        }

        public async Task<Employee> GetByEmailIdAsync(string emailId)
        {
            try
            {
                return await _context.Employees
                    .Include(x => x.EmployeeAuth)
                    .FirstOrDefaultAsync(x => x.EmployeeEmail == emailId) 
                    ?? throw new EntityNotFoundException<Employee>(emailId);
            }
            catch (EntityNotFoundException<Employee>)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Employee by Email Id", ex);
            }
        }
        [ExcludeFromCodeCoverage]
        public override Task<bool> IsDuplicate(Employee entity)
        {
            return _context.Employees.AnyAsync(x => x.EmployeeEmail == entity.EmployeeEmail);
        }
    }
}
