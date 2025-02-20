using System.Net.Http.Json;
using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Constants;
using FashionStoreWebApp.Helpers;

namespace FashionStoreWebApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public async Task<UserVm> GetUserById(string userId)
        {
            var response = await _httpClient.GetAsync($"/api/Users/{userId}");
            var userJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UserVm>(userJson, ConstantValues.jsonSerializerOptions);
            return result ?? new UserVm();
        }

        public async Task<FormResult> CreateUserAsync(UserCreationDTO userCreation)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Users", userCreation);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }
        public async Task<FormResult> UpdateUserAsync(UserCreationDTO userDto)
        {
            var result = await _httpClient.PutAsJsonAsync("/api/Users", userDto);
            if (result.IsSuccessStatusCode)
            {
                return new FormResult { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(result);
        }
        public async Task<FormResult> DeleteUserAsync(string userId)
        {
            var response = await _httpClient.DeleteAsync($"/api/Users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return new FormResult() { Succeeded = true };
            }
            return await ErrorResponseHelper.ConvertToFormResultError(response);
        }

        public async Task<PagingData<UserVm>> GetPagedUsersAsync(UserSearchRequest userSearch, int pageIndex, int pageSize)
        {
            var response = await _httpClient.GetAsync($"/api/Users?name={userSearch.Name}&email={userSearch.Email}&pageNumber={pageIndex}&pageSize={pageSize}");
            var usersJson = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PagingData<UserVm>>(usersJson, ConstantValues.jsonSerializerOptions);
            return result ?? new PagingData<UserVm>(new List<UserVm>(), 0, pageIndex, pageSize);
        }

    }
}
