using FashionStoreWebApi.Models;
using FashionStoreViewModel;

namespace FashionStoreWebApi.Services
{
    public interface ICartService
    {
        Task<CartVm> AddToCartAsync(string userId, long productId, long quantity);
        Task<IList<CartVm>> GetCartItemsByUserId(string userId);
        Task<bool> UpdateCartItemQuantityAsync(long cartItemId, long newQuantity);
        Task<bool> RemoveFromCartAsync(long cartItemId);
        Task<bool> ClearCartAsync(string userId);
    }
}
