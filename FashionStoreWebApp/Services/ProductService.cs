using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Constants;
using FashionStoreWebApp.Helpers;

namespace FashionStoreWebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<PagingData<ProductVm>> SearchProduct(ProductSearchRequest productSearch, int pageIndex, 
            int pageSize, string sortBy = "Id", bool isAscending = false)
        {
            var response = await _httpClient.GetAsync(
                $"/api/Products?name={productSearch.name}&categoryId={productSearch.categoryId}" +
                $"&brandId={productSearch.brandId}&pageNumber={pageIndex}&pageSize={pageSize}" +
                $"&sortBy={sortBy}&isAscending={isAscending}"
            );
            var productJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PagingData<ProductVm>>(productJson, ConstantValues.jsonSerializerOptions);
            return result ?? new PagingData<ProductVm>(new List<ProductVm>(), 0, pageIndex, pageSize);
        }

        public async Task<ProductVm> GetByIdAsync(long productId)
        {
            var response = await _httpClient.GetAsync($"/api/Products/{productId}");
            var productJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductVm>(productJson, ConstantValues.jsonSerializerOptions);
        }

        public async Task<FormResult> CreateProductAsync(MultipartFormDataContent productCreation)
        {
            var result = await _httpClient.PostAsync("/api/Products", productCreation);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }

        public async Task<FormResult> UpdateProductAsync(MultipartFormDataContent productDto)
        {
            var result = await _httpClient.PutAsync("/api/Products", productDto);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }

        public async Task<FormResult> DeleteProductAsync(long productId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Products/{productId}");
            if (response.IsSuccessStatusCode)
            {
                return new FormResult() { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(response);
        }
    }
}
