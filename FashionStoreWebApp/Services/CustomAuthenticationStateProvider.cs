using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using FashionStoreViewModel;
using Microsoft.AspNetCore.Components.Authorization;

namespace FashionStoreWebApp.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAccountManagementService
    {
        private bool _isAuthenticated = false;
        private readonly ClaimsPrincipal Unauthenticated = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Auth");
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return null;
        }

        public async Task<FormResult> RegisterAsync(RegisterDTO registerInfo)
        {
            string[] defaultDetail = ["An unknown error prevented registration from succeeding."];
            try
            {
                var result = await _httpClient.PostAsJsonAsync("register", registerInfo);
                if (result.IsSuccessStatusCode)
                {
                    return new FormResult { Succeeded = true };
                }

                var errors = new List<string>();
                var details = await result.Content.ReadAsStringAsync();
                var problemDetails = JsonDocument.Parse(details);
                var errorList = problemDetails.RootElement.GetProperty("errors");

                foreach (var errorEntry in errorList.EnumerateObject())
                {
                    if (errorEntry.Value.ValueKind == JsonValueKind.String)
                    {
                        errors.Add(errorEntry.Value.GetString()!);
                    }
                    else if (errorEntry.Value.ValueKind == JsonValueKind.Array)
                    {
                        errors.AddRange(
                            errorEntry.Value.EnumerateArray().Select(
                                e => e.GetString() ?? string.Empty)
                            .Where(e => !string.IsNullOrEmpty(e)));
                    }
                }
                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = problemDetails == null ? defaultDetail : [.. errors]
                };
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
