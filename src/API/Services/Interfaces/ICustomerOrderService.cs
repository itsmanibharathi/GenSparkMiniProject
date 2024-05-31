using API.Models.DTOs.CustomerDto;

namespace API.Services.Interfaces
{
    public interface ICustomerOrderService
    {
        public Task<IEnumerable<ReturnCustomerOrderDto>> CreateOrder(CustomerOrderDto createCustomerOrderDto);
        public Task<ReturnOrderPaymentDto> CreatePayment(CustomerOrderPaymentDto orderPaymentDto);
        public Task<ReturnCustomerOrderDto> GetOrder(int customerId, int orderId);
        public Task<IEnumerable<ReturnCustomerOrderDto>> GetAllOrders(int customerId);
    }
}
