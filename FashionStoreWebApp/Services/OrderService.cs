using System.Net.Http.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Helpers;

namespace FashionStoreWebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<FormResult> PlaceOrderAsync(OrderVm orderVm)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Orders", orderVm);
            if (response.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(response);
        }
    }
}
