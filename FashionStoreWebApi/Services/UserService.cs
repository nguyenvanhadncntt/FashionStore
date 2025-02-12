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

            // Check the role is existing
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

        public async Task<UserCreationDTO> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            return new UserCreationDTO
            {
                Id = userId,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault()
            };
        }

        public async Task<IdentityResult> UpdateUserAsync(string userId, UserCreationDTO userDto)
        {
            User? user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.FullName = userDto.FirstName + " " + userDto.LastName;
            user.PhoneNumber = userDto.PhoneNumber;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var changePasswordResult = await _userManager.ResetPasswordAsync(user, token, userDto.Password);
                if (!changePasswordResult.Succeeded)
                    return changePasswordResult;
            }

            if (!string.IsNullOrEmpty(userDto.Role))
            {

                if (!await _roleManager.RoleExistsAsync(userDto.Role))
                {
                    throw new Exception("Role does not exist.");
                }

                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Any() && !roles.Contains(userDto.Role))
                {
                    var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, roles);
                    if (!removeRoleResult.Succeeded)
                        return removeRoleResult;

                    return await _userManager.AddToRoleAsync(user, userDto.Role);
                }
            }
            return await _userManager.UpdateAsync(user);


        }
    }
}
