using API.Models;

namespace API.Repositories.Interfaces
{
    public interface IRepository<k, T> where T : class
    {
        public Task<T> Add(T entity);
        public Task<T> Update(T entity);
        public Task<bool> Delete(k id);
        public Task<T> Get(k id);
        public Task<IEnumerable<T>> Get();
        Task<Customer> Add(Customer customer);
    }
}
