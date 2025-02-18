using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IAccountManagementService
    {
        public Task<FormResult> RegisterAsync(RegisterDTO registerInfo);
        public Task<FormResult> LoginAsync(string email, string password);
        public Task LogoutAsync();
    }
}
