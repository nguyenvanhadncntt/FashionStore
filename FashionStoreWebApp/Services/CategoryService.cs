using System.Net.Http.Json;
using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Constants;
using FashionStoreWebApp.Helpers;

namespace FashionStoreWebApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<CategoryVm> GetCategoryByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"/api/Categories/{id}");
            var categoryJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CategoryVm>(categoryJson, ConstantValues.jsonSerializerOptions);
            return result ?? new CategoryVm();
        }

        public async Task<PagingData<CategoryVm>> GetCategoriesAsync(string name, int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Categories?name={name}&PageNumber={pageNumber}&pageSize={pageSize}");
            var categoriesJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PagingData<CategoryVm>>(categoriesJson, ConstantValues.jsonSerializerOptions);
            return result ?? new PagingData<CategoryVm>([], 0, pageNumber, pageSize);
        }

        public async Task<FormResult> CreateCategoryAsync(CategoryVm categoryVm)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Categories", categoryVm);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }

        public async Task<FormResult> DeleteCategoryAsync(long categoryId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Categories/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                return new FormResult() { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(response);
        }

        public async Task<FormResult> UpdateCategoryAsync(CategoryVm categoryVm)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Categories", categoryVm);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }


    }
}
