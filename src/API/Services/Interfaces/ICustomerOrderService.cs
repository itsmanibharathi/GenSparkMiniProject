using API.Models.DTOs;

namespace API.Services.Interfaces
{
    public interface ICustomerOrderService
    {
        public Task<IEnumerable<ReturnCreateCustomerOrderDto>> CreateOrder(CreateCustomerOrderDto createCustomerOrderDto);
        public Task<ReturnOrderPaymentDto> CreatePayment(OrderPaymentDto orderPaymentDto);
        public Task<ReturnCreateCustomerOrderDto> GetOrder(int customerId, int orderId);
        public Task<IEnumerable<ReturnCreateCustomerOrderDto>> GetAllOrders(int customerId);
    }
}
