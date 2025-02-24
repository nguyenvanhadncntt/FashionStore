using FashionStoreViewModel;
using FashionStoreViewModel.Enumerations;

namespace FashionStoreWebApp.Services
{
    public interface IOrderService
    {
        Task<FormResult> PlaceOrderAsync(OrderVm orderVm);
        Task<PagingData<OrderVm>> GetOrdersAsync(string name, int pageNumber, int pageSize);
        Task<OrderVm> GetOrderDetailAsync(long orderId);
        Task<IList<OrderVm>> GetMyOrdersAsync();
        Task<FormResult> UpdateOrderStatusAsync(long orderId, OrderStatus status);
        Task<FormResult> UpdatePaymentStatusAsync(long orderId, PaymentStatus status);
        Task<FormResult> DeleteOrderAsync(long orderId);
    }
}
