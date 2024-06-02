using API.Models.DTOs.CustomerDto;

namespace API.Services.Interfaces
{
    public interface ICustomerOrderService
    {
        public Task<IEnumerable<ReturnCustomerOrderDto>> CreateOrder(CustomerOrderDto createCustomerOrderDto);
        public Task<ReturnOrderOnlinePaymentDto> CreateOnlinePayment(CustomerOrderPaymentDto orderPaymentDto);
        public Task<IEnumerable<ReturnOrderCashPaymentDto>> CreateCashPayment(CustomerOrderPaymentDto orderPaymentDto);
        public Task<ReturnCustomerOrderDto> GetOrder(int customerId, int orderId);
        public Task<IEnumerable<ReturnCustomerOrderDto>> GetAllOrders(int customerId);
    }
}
