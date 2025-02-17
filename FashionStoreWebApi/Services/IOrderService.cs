using FashionStoreWebApi.Models.DTOs;
using FashionStoreViewModel.Enumerations;
using FashionStoreViewModel;

namespace FashionStoreWebApi.Services
{
    public interface IOrderService
    {
        Task PlaceOrderAsync(string userId, OrderVm orderVm);
        Task<OrderVm> GetOrderDetailAsync(long orderId);
        Task<IList<OrderVm>> GetMyOrdersAsync(string userId);
        Task UpdateOrderStatusAsync(long orderId, OrderStatus status);
        Task UpdatePaymentStatusAsync(long orderId, PaymentStatus status);
        Task DeleteOrderAsync(long orderId);
        Task<PagingData<OrderVm>> GetOrdersAsync(PagingRequest pagingRequest);
    }
}
