using System.Net.Http.Json;
using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Helpers;
using FashionStoreWebApp.Constants;

namespace FashionStoreWebApp.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;
        public RoleService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<FormResult> AddRoleAsync(string role)
        {
            var result = await _httpClient.PostAsJsonAsync($"/api/Roles", new RoleVm() { Name = role});
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }

        public async Task<FormResult> DeleteRole(string roleId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Roles/{roleId}");
            if (response.IsSuccessStatusCode)
            {
                return new FormResult() { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(response);
        }

        public async Task<List<RoleVm>> GetRolesAsync()
        {
            var response = await _httpClient.GetAsync("/api/Roles");
            var rolesJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<RoleVm>>(rolesJson, ConstantValues.jsonSerializerOptions);
            return result ?? new List<RoleVm>();
        }

        public async Task<FormResult> UpdateRole(RoleVm roleVm)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/Roles", roleVm);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }

            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }

        public async Task<RoleVm> GetRoleByIdAsync(string roleId)
        {
            var response = await _httpClient.GetAsync($"/api/Roles/{roleId}");
            var rolesJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<RoleVm>(rolesJson, ConstantValues.jsonSerializerOptions);
            return result ?? new RoleVm();
        }

    }
}
