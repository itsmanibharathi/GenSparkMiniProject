using API.Models.DTOs.EmployeeDto;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    /// <summary>
    /// Interface for managing employees.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Authenticates an employee based on login credentials.
        /// </summary>
        /// <param name="employeeLoginDto">The employee login DTO containing email and password.</param>
        /// <returns>The logged-in employee DTO with a token.</returns>
        /// <exception cref="EntityNotFoundException{Employee}">Thrown when the employee is not found.</exception>
        /// <exception cref="InvalidUserCredentialException">Thrown when the credentials are invalid.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        Task<ReturnEmployeeLoginDto> Login(EmployeeLoginDto employeeLoginDto);

        /// <summary>
        /// Registers a new employee.
        /// </summary>
        /// <param name="employeeRegisterDto">The employee register DTO containing employee details.</param>
        /// <returns>The registered employee DTO.</returns>
        /// <exception cref="EntityAlreadyExistsException{Employee}">Thrown when the employee already exists.</exception>
        /// <exception cref="UnableToDoActionException">Thrown when unable to perform the action.</exception>
        Task<ReturnEmployeeRegisterDto> Register(EmployeeRegisterDto employeeRegisterDto);
    }
}
