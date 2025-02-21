using FashionStoreWebApi.Models;
using FashionStoreViewModel;

namespace FashionStoreWebApi.Services
{
    public interface ICartService
    {
        Task<CartVm> AddToCartAsync(string userId, long productId, int quantity);
        Task<IList<CartVm>> GetCartItemsByUserId(string userId);
        Task<bool> UpdateCartItemQuantityAsync(long cartItemId, int newQuantity);
        Task<bool> RemoveFromCartAsync(long cartItemId);
        Task<bool> ClearCartAsync(string userId);
    }
}
