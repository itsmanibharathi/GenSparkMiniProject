using API.Models.DTOs.CustomerDto;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Interface for customer services.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Authenticates a customer and generates a token.
        /// </summary>
        /// <param name="customerLoginDto">The DTO containing the customer's login credentials.</param>
        /// <returns>The DTO containing the customer's details and token.</returns>
        /// <exception cref="InvalidUserCredentialException">Thrown when the customer's credentials are invalid.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs during login.</exception>
        Task<ReturnCustomerLoginDto> Login(CustomerLoginDto customerLoginDto);

        /// <summary>
        /// Registers a new customer.
        /// </summary>
        /// <param name="customerRegisterDto">The DTO containing the customer's registration details.</param>
        /// <returns>The DTO containing the registered customer's details.</returns>
        /// <exception cref="EntityAlreadyExistsException{Customer}">Thrown when the customer already exists.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when an error occurs during registration.</exception>
        Task<ReturnCustomerRegisterDto> Register(CustomerRegisterDto customerRegisterDto);

        /// <summary>
        /// Updates the phone number of an existing customer.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <param name="phone">The new phone number of the customer.</param>
        /// <returns>The DTO containing the updated customer's details.</returns>
        /// <exception cref="EntityNotFoundException{Customer}">Thrown when the customer is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during the update.</exception>
        Task<ReturnCustomerDto> UpdatePhone(int customerId, string phone);

        /// <summary>
        /// Retrieves the details of a customer by ID.
        /// </summary>
        /// <param name="customerId">The ID of the customer.</param>
        /// <returns>The DTO containing the customer's details.</returns>
        /// <exception cref="EntityNotFoundException{Customer}">Thrown when the customer is not found.</exception>
        /// <exception cref="Exception">Thrown when an error occurs during retrieval.</exception>
        Task<ReturnCustomerDto> Get(int customerId);
    }
}
}
