using API.Exceptions;
using API.Models;
using API.Models.DTOs;
using API.Models.DTOs.CustomerDto;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    /// <summary>
    /// Service class for managing customer addresses.
    /// </summary>
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly ICustomerAddressRepository _repository;
        private readonly IMapper _mapper;

        public CustomerAddressService(ICustomerAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new customer address.
        /// </summary>
        /// <param name="addCustomerAddressDto">The DTO containing data for the new customer address.</param>
        /// <returns>The DTO of the added customer address.</returns>
        /// <exception cref="EntityAlreadyExistsException{CustomerAddress}">Thrown when the customer address already exists.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnCustomerAddressDto> Add(CustomerAddressDto addCustomerAddressDto)
        {
            try
            {
                var res = await _repository.AddAsync(_mapper.Map<CustomerAddress>(addCustomerAddressDto));
                return _mapper.Map<ReturnCustomerAddressDto>(res);
            }
            catch (EntityAlreadyExistsException<CustomerAddress>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a customer address.
        /// </summary>
        /// <param name="CustomerId">The ID of the customer owning the address.</param>
        /// <param name="CustomerAddressId">The ID of the customer address to delete.</param>
        /// <returns>A message indicating the result of the delete operation.</returns>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer address is not found.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<string> Delete(int CustomerId, int CustomerAddressId)
        {
            try
            {
                var res = await _repository.DeleteAsync(CustomerId, CustomerAddressId);
                return res ? "Address Deleted Successfully" : throw new EntityNotFoundException<CustomerAddress>(CustomerAddressId);
            }
            catch (EntityNotFoundException<CustomerAddress>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all customer addresses for a given customer ID.
        /// </summary>
        /// <param name="CustomerId">The ID of the customer.</param>
        /// <returns>A collection of customer address DTOs.</returns>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer does not have any addresses.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<IEnumerable<ReturnCustomerAddressDto>> Get(int CustomerId)
        {
            try
            {
                var res = await _repository.GetByCustomerIdAsync(CustomerId);
                return _mapper.Map<IEnumerable<ReturnCustomerAddressDto>>(res);
            }
            catch (EntityNotFoundException<CustomerAddress>)
            {
                throw new EntityNotFoundException<CustomerAddress>($"User {CustomerId} does not have any addresses");
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets a specific customer address for a given customer ID and address ID.
        /// </summary>
        /// <param name="CustomerId">The ID of the customer.</param>
        /// <param name="CustomerAddressId">The ID of the customer address to retrieve.</param>
        /// <returns>The DTO of the retrieved customer address.</returns>
        /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer address is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when the user does not belong to the customer address.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        public async Task<ReturnCustomerAddressDto> Get(int CustomerId, int CustomerAddressId)
        {
            try
            {
                var res = await _repository.GetAsync(CustomerAddressId);
                if (res.CustomerId != CustomerId)
                {
                    throw new UnauthorizedAccessException($"User {CustomerId} does not belong to this Customer Address {CustomerAddressId}");
                }
                return _mapper.Map<ReturnCustomerAddressDto>(res);
            }
            catch (EntityNotFoundException<CustomerAddress>)
            {
                throw;
            }
            catch (UnableToDoActionException)
            {
                throw;
            }
        }
    }
}
