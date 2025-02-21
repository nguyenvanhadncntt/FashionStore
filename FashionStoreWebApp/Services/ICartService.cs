using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface ICartService
    {
        Task<FormResult> AddToCart(CartVm cartVm);
        Task<FormResult> RemoveCartById(long cartId);
        Task<IList<CartVm>> GetCartItems();
        Task<FormResult> UpdateCartItemQuantity(CartVm cart);
    }
}
