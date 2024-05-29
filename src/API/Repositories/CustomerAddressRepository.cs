using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace API.Repositories
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        protected readonly DBGenSparkMinirojectContext _context;

        public CustomerAddressRepository(DBGenSparkMinirojectContext context)
        {
            _context = context;
        }
        public virtual async Task<CustomerAddress> Add(CustomerAddress entity)
        {
            try
            {
                _ = Get(entity.CustomerId);
                if (!await IsDuplicate(entity))
                {
                    _context.CustomerAddresses.Add(entity);
                    var res = await _context.SaveChangesAsync();
                    return res > 0 ? entity : throw new UnableToDoActionException("Unable to insert");
                }
                throw new DataDuplicateException();
            }
            catch (CustomerNotFoundException)
            { 
                throw;
            }
            catch (DataDuplicateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to Insert the new CustomerAddress", ex);
            }
        }

        private async Task<bool> IsDuplicate(CustomerAddress entity)
        {
            var CustomerAddress = await _context.CustomerAddresses.FirstOrDefaultAsync(ca => ca.CustomerId == entity.CustomerId && ca.Type == entity.Type && ca.Code == entity.Code);
            return CustomerAddress != null;
        }

        public virtual async Task<bool> Delete(int id)
        {
            _context.CustomerAddresses.Remove(await Get(id));
            var res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public virtual async Task<CustomerAddress> Get(int id)
        {
            try
            {

                return (await _context.CustomerAddresses.FirstOrDefaultAsync(c => c.AddressId == id)) 
                    ?? throw new CustomerAddressNotFoundException();
            }
            catch (CustomerAddressNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the CustomerAddress", ex);
            }
        }

        public virtual async Task<IEnumerable<CustomerAddress>> Get()
        {
            try
            {
                var res = await _context.CustomerAddresses.ToListAsync();
                return res.Count > 0 ? res : throw new EmptyDatabaseException("CustomerAddress DB Empty");
            }
            catch (EmptyDatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the CustomerAddresss", ex);
            }
        }

        public virtual async Task<CustomerAddress> Update(CustomerAddress entity)
        {
            try
            {
                _context.CustomerAddresses.Update(entity);
                var res = await _context.SaveChangesAsync();
                return res > 0 ? entity : throw new UnableToDoActionException("Unable to update");
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to update the CustomerAddress", ex);
            }
        }

        public async Task<CustomerAddress> Get(int CustomerId, int CustomerAddressId)
        {
            try
            {
                var res = await _context.CustomerAddresses.FirstOrDefaultAsync(ca => ca.CustomerId == CustomerId && ca.AddressId == CustomerAddressId);
                return res ?? throw new CustomerAddressNotFoundException();
            }
            catch (CustomerAddressNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the CustomerAddress", ex);
            }
        }

        public async Task<IEnumerable<CustomerAddress>> GetByCustomerId(int CustomerId)
        {
            try
            {
                var res = await _context.CustomerAddresses.Where(ca => ca.CustomerId == CustomerId).ToListAsync();
                return res.Count > 0 ? res : throw new CustomerAddressNotFoundException();
                
            }
            catch (CustomerAddressNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to get the CustomerAddress", ex);
            }
        }
    }
}
