using API.Context;
using API.Exceptions;
using API.Models;
using API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
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

        /// <summary>
        /// Add Customer Address
        /// </summary>
        /// <param name="entity">New Customer Address Object</param>
        /// <returns>Return  </returns>
        /// <exception cref="UnableToDoActionException">dd</exception>
        public virtual async Task<CustomerAddress> Add(CustomerAddress entity)
        {
            try
            {
                if (!await IsDuplicate(entity))
                {
                    _context.CustomerAddresses.Add(entity);
                    var res = await _context.SaveChangesAsync();
                    return res > 0 ? entity : throw new UnableToDoActionException("Unable to insert");
                }
                throw new DataDuplicateException("Address Already exist");
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

        [ExcludeFromCodeCoverage]
        private async Task<bool> IsDuplicate(CustomerAddress entity)
        {
            var CustomerAddress = await _context.CustomerAddresses.FirstOrDefaultAsync(ca => ca.CustomerId == entity.CustomerId && ca.Type == entity.Type && ca.Code == entity.Code);
            return CustomerAddress != null;
        }


        /// <summary>
        /// Delete Customer Address by Id
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="CustomerAddressId"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(int CustomerId, int CustomerAddressId)
        {
            try
            {

                _context.CustomerAddresses.Remove(await Get(CustomerId,CustomerAddressId));
                var res = await _context.SaveChangesAsync();
                return res > 0;
            }
            catch (CustomerAddressNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UnableToDoActionException("Unable to delete the CustomerAddress", ex);
            }
        }
        [ExcludeFromCodeCoverage]
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

        public async Task<IEnumerable<CustomerAddress>> Get(int CustomerId)
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
