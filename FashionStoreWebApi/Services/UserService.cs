using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace FashionStoreWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserCreationDTO userCreation)
        {
            // Create the user
            User user = new User
            {
                UserName = userCreation.UserName,
                Email = userCreation.Email,
                FullName = userCreation.FirstName + " " + userCreation.LastName,
                PhoneNumber = userCreation.PhoneNumber,
            };
            await _userManager.CreateAsync(user, userCreation.Password);

            // Ensure the role exists
            if (!await _roleManager.RoleExistsAsync(userCreation.Role))
            {
                throw new Exception("Role does not exist.");
            }

            return await _userManager.AddToRoleAsync(user, userCreation.Role);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.DeleteAsync(user);
        }
    }
}
