using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IOrderService
    {
        Task<FormResult> PlaceOrderAsync(OrderVm orderVm);
    }
}
