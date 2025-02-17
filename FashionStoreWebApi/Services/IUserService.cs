using FashionStoreWebApi.Models;
using FashionStoreViewModel;
using Microsoft.AspNetCore.Identity;

namespace FashionStoreWebApi.Services
{
    public interface IUserService
    {
        Task<PagingData<UserVm>> SearchUser(UserSearchRequest userSearch, PagingRequest pagingRequest);
        Task<IdentityResult> CreateUserAsync(UserCreationDTO user);
        Task<IdentityResult> UpdateUserAsync(string userId, UserCreationDTO user);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<UserVm> GetUserById(string userId);
    }
}
