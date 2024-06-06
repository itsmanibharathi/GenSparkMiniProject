using API.Models.DTOs.CustomerDto;

/// <summary>
/// Interface for managing customer addresses.
/// </summary>
public interface ICustomerAddressService
{
    /// <summary>
    /// Adds a new customer address.
    /// </summary>
    /// <param name="addCustomerAddressDto">The DTO containing data for the new customer address.</param>
    /// <returns>The DTO of the added customer address.</returns>
    /// <exception cref="EntityAlreadyExistsException{CustomerAddress}">Thrown when the customer address already exists.</exception>
    /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
    public Task<ReturnCustomerAddressDto> Add(CustomerAddressDto addCustomerAddressDto);

    /// <summary>
    /// Deletes a customer address.
    /// </summary>
    /// <param name="CustomerId">The ID of the customer owning the address.</param>
    /// <param name="CustomerAddressId">The ID of the customer address to delete.</param>
    /// <returns>A message indicating the result of the delete operation.</returns>
    /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer address is not found.</exception>
    /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
    public Task<string> Delete(int CustomerId, int CustomerAddressId);

    /// <summary>
    /// Gets all customer addresses for a given customer ID.
    /// </summary>
    /// <param name="CustomerId">The ID of the customer.</param>
    /// <returns>A collection of customer address DTOs.</returns>
    /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer does not have any addresses.</exception>
    /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
    public Task<IEnumerable<ReturnCustomerAddressDto>> Get(int CustomerId);

    /// <summary>
    /// Gets a specific customer address for a given customer ID and address ID.
    /// </summary>
    /// <param name="CustomerId">The ID of the customer.</param>
    /// <param name="CustomerAddressId">The ID of the customer address to retrieve.</param>
    /// <returns>The DTO of the retrieved customer address.</returns>
    /// <exception cref="EntityNotFoundException{CustomerAddress}">Thrown when the customer address is not found.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when the user does not belong to the customer address.</exception>
    /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
    public Task<ReturnCustomerAddressDto> Get(int CustomerId, int CustomerAddressId);
}