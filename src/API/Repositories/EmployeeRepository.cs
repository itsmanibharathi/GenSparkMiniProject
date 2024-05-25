using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace API.Repositories
{
    public class EmployeeRepository : IRepository<int, Employee>
    {
        protected readonly DBGenSparkMinirojectContext _context;

        public EmployeeRepository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }
        public virtual async Task<Employee> Add(Employee entity)
        {
            try
            {
                if (!await IsDuplicate(entity))
                {
                    _context.Employees.Add(entity);
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
                throw new UnableToDoActionException("Unable to Insert the new Employee", ex);
            }
        }

        private async Task<bool> IsDuplicate(Employee entity)
        {
            var Employee = await _context.Employees.FirstOrDefaultAsync(c => c.EmployeeEmail == entity.EmployeeEmail && c.EmployeePhone == entity.EmployeePhone);
            return Employee != null;
        }

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Employee> Get(int id)
        {
            try
            {

                return (await _context.Employees.FirstOrDefaultAsync(c => c.EmployeeId == id)) ?? throw new EmployeeNotFoundException();
            }
            catch (EmployeeNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Employee", ex);
            }
        }

        public virtual async Task<IEnumerable<Employee>> Get()
        {
            try
            {
                var res = await _context.Employees.ToListAsync();
                return res.Count > 0 ? res : throw new EmptyDatabaseException("Employee DB Empty");
            }
            catch (EmptyDatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the Employees", ex);
            }
        }

        public virtual async Task<Employee> Update(Employee entity)
        {
            try
            {
                _context.Employees.Update(entity);
                var res = await _context.SaveChangesAsync();
                return res > 0 ? entity : throw new UnableToDoActionException("Unable to update");
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to update the Employee", ex);
            }
        }
    }
}
