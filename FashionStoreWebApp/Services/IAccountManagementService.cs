using FashionStoreViewModel;
using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IAccountManagementService
    {
        public Task<FormResult> RegisterAsync(RegisterDTO registerInfo);
    }
}
