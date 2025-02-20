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

        public async Task<PagingData<ProductVm>> SearchProduct(string productName, int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync(
                $"/api/Products?name={productName}&pageNumber={pageIndex}&pageSize={pageSize}"
            );
            var productJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PagingData<ProductVm>>(productJson, ConstantValues.jsonSerializerOptions);
            return result ?? new PagingData<ProductVm>(new List<ProductVm>(), 0, pageIndex, pageSize);
        }

        //public async Task<FormResult> CreateProductAsync(ProductCreationDTO productCreation)
        //{
        //    var result = await _httpClient.PostAsJsonAsync("/api/Products", productCreation);
        //    if (result.IsSuccessStatusCode)
        //    {
        //        return new FormResult { Succeeded = true };
        //    }
        //    return await ErrorResponseHelper.ConvertToFormResultError(result);
        //}

        //public async Task<FormResult> UpdateProductAsync(ProductCreationDTO productDto)
        //{
        //    var result = await _httpClient.PutAsJsonAsync("/api/Products", productDto);
        //    if (result.IsSuccessStatusCode)
        //    {
        //        return new FormResult { Succeeded = true };
        //    }
        //    return await ErrorResponseHelper.ConvertToFormResultError(result);
        //}

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
