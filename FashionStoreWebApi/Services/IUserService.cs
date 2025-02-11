using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FashionStoreWebApi.Services
{
    public interface IUserService
    {
        //Task<List<UserModel>> GetAllUsers();

        //Task<UserModel> GetUserByEmail(string emailId);

        //Task<bool> UpdateUser(string emailId, UserModel user);

        //Task<bool> DeleteUserByEmail(string emailId);

        // Create a new user
        Task<IdentityResult> CreateUserAsync(UserCreationDTO user);

        Task<IdentityResult> DeleteUserAsync(string email);
    }
}
