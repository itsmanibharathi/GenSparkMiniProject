using API.Exceptions;
using API.Interfaces;
using API.Models;
using API.Repositories;

namespace API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<int, CustomerRepository> _repository;

        public CustomerService(IRepository<int,CustomerRepository> repository) {
            _repository = repository;
        }
        public Task<Customer> Regiser(Customer customer)
        {
            return _repository.Add(customer);
        }
    }
}
