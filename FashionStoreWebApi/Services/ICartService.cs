using FashionStoreWebApi.Models;
using FashionStoreViewModel;

namespace FashionStoreWebApi.Services
{
    public interface ICartService
    {
        Task<Cart> AddToCartAsync(string userId, long productId, long quantity);
        Task<IList<CartVm>> GetCartItemsByUserId(string userName);
        Task<bool> UpdateCartItemQuantityAsync(long cartItemId, long newQuantity);
        Task<bool> RemoveFromCartAsync(int cartItemId);
        Task<bool> ClearCartAsync(string userName);
    }
}
