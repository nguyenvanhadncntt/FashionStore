using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FashionStoreWebApi.Services
{
    public interface IUserService
    {
        // Create a new user
        Task<IdentityResult> CreateUserAsync(UserCreationDTO user);
        Task<IdentityResult> UpdateUserAsync(string userId, UserCreationDTO user);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<UserCreationDTO> GetUserById(string userId);
    }
}
