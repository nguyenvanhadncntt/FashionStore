using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using FashionStoreViewModel;
using FashionStoreWebApp.Constants;
using FashionStoreWebApp.Helpers;
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
            _isAuthenticated = false;

            // default to not authenticated
            var user = Unauthenticated;

            try
            {
                var userResponse = await _httpClient.GetAsync("api/Users/current-user");

                userResponse.EnsureSuccessStatusCode();

                var userJson = await userResponse.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserVm>(userJson, ConstantValues.jsonSerializerOptions);

                if (userInfo != null)
                {
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, userInfo.Email),
                        new(ClaimTypes.Email, userInfo.Email),
                        new(ClaimTypes.Role, userInfo.Role)
                    };

                    var id = new ClaimsIdentity(claims, nameof(CustomAuthenticationStateProvider));

                    user = new ClaimsPrincipal(id);
                    _isAuthenticated = true;
                }
            }
            catch (Exception ex)
            {
                
            }

            return new AuthenticationState(user);
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

                var errors = ErrorResponseHelper.GetErrorMsgFromResponse(await result.Content.ReadAsStringAsync());

                return new FormResult
                {
                    Succeeded = false,
                    ErrorList = !errors.Any() ? defaultDetail : [.. errors]
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<FormResult> LoginAsync(string email, string password)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync(
                    "login?useCookies=true", new
                    {
                        email,
                        password
                    });

                if (result.IsSuccessStatusCode)
                {
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    return new FormResult { Succeeded = true };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new FormResult
            {
                Succeeded = false,
                ErrorList = ["Invalid email and/or password."]
            };
        }

        public async Task LogoutAsync()
        {
            const string Empty = "{}";
            var emptyContent = new StringContent(Empty, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("api/Users/logout", emptyContent);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        }
    }
}
