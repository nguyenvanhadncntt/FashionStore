using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreViewModel.Enumerations;
using FashionStoreWebApp.Constants;
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

        public async Task<PagingData<OrderVm>> GetOrdersAsync(string name, int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"api/Orders?name={name}&pageNumber={pageNumber}&pageSize={pageSize}");
            var orderJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PagingData<OrderVm>>(orderJson, ConstantValues.jsonSerializerOptions);
            return result ?? new PagingData<OrderVm>([], 0, pageNumber, pageSize);
        }

        public async Task<OrderVm> GetOrderDetailAsync(long orderId)
        {
            var response = await _httpClient.GetAsync($"api/Orders/{orderId}");
            var orderJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OrderVm>(orderJson, ConstantValues.jsonSerializerOptions);
        }

        public async Task<IList<OrderVm>> GetMyOrdersAsync()
        {
            var response = await _httpClient.GetAsync($"api/Orders/my-orders");
            var myOrdersJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IList<OrderVm>>(myOrdersJson, ConstantValues.jsonSerializerOptions);

        }

        public async Task<FormResult> UpdateOrderStatusAsync(long orderId, OrderStatus status)
        {
            var response = await _httpClient.PutAsync($"api/Orders/update-status/{orderId}/{status}", null);
            if (response.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(response);
        }

        public async Task<FormResult> UpdatePaymentStatusAsync(long orderId, PaymentStatus status)
        {
            var response = await _httpClient.PutAsync($"api/Orders/update-payment-status/{orderId}/{status}", null);
            if (response.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(response);
        }

        public async Task<FormResult> DeleteOrderAsync(long orderId)
        {
            var response = await _httpClient.DeleteAsync($"api/Orders/{orderId}");
            if (response.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(response);
        }
    }
}
