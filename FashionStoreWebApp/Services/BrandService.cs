using System.Net.Http.Json;
using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Constants;
using FashionStoreWebApp.Helpers;

namespace FashionStoreWebApp.Services
{
    public class BrandService: IBrandService
    {
        private readonly HttpClient _httpClient;
        public BrandService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<FormResult> CreateBrandAsync(BrandVm brandVm)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Brands", brandVm);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }

        public async Task<FormResult> Delete(long id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Brands/{id}");
            if (response.IsSuccessStatusCode)
            {
                return new FormResult() { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(response);

        }

        public async Task<BrandVm> GetBrandByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"/api/Brands/{id}");
            var brandJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<BrandVm>(brandJson, ConstantValues.jsonSerializerOptions);
            return result ?? new BrandVm();
        }

        public async Task<PagingData<BrandVm>> GetBrands(string name, int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Brands?name={name}&pageNumber={pageIndex}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            var brandJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PagingData<BrandVm>>(brandJson, ConstantValues.jsonSerializerOptions);
            return result ?? new PagingData<BrandVm>([], 0, pageIndex, pageSize);
        }

        public async Task<FormResult> UpdateBrandAsync(BrandVm brandVm)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Brands", brandVm);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }
    }
}
