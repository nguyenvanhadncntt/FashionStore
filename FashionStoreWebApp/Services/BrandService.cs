using System.Net.Http.Json;
using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Constants;

namespace FashionStoreWebApp.Services
{
    public class BrandService: IBrandService
    {
        private readonly HttpClient _httpClient;
        public BrandService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<PagingData<BrandVm>> getBrands(string name, int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Brands?name={name}&pageIndex={pageIndex}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();
            var brandJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PagingData<BrandVm>>(brandJson, ConstantValues.jsonSerializerOptions);
            return result ?? new PagingData<BrandVm>([], 0, pageIndex, pageSize);
        }
    }
}
