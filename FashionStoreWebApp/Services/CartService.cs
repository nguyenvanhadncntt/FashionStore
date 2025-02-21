using System.Net.Http.Json;
using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Constants;
using FashionStoreWebApp.Helpers;

namespace FashionStoreWebApp.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<FormResult> AddToCart(CartVm cartVm)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Carts", cartVm);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult() { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }

        public async Task<IList<CartVm>> GetCartItems()
        {
            var response = await _httpClient.GetAsync("/api/Carts");
            var cartJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<CartVm>>(cartJson, ConstantValues.jsonSerializerOptions);
            return result ?? new List<CartVm>();
        }

        public async Task<FormResult> RemoveCartById(long cartId)
        {
            var result = await _httpClient.DeleteAsync($"/api/Carts/{cartId}");
            if (result.IsSuccessStatusCode)
            {
                return new FormResult() { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }

        public async Task<FormResult> UpdateCartItemQuantity(CartVm cart)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Carts", cart);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult() { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }
    }
}
