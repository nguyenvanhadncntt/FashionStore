using System.Reflection.Metadata.Ecma335;
using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            User user = ConvertToUserEntity(userCreation);
            await _userManager.CreateAsync(user, userCreation.Password);

            // Check the role is existing
            if (!await _roleManager.RoleExistsAsync(userCreation.Role))
            {
                throw new Exception("Role does not exist.");
            }

            return await _userManager.AddToRoleAsync(user, userCreation.Role);
        }

        private static User ConvertToUserEntity(UserCreationDTO userCreation)
        {
            // Create the user
            return new User
            {
                UserName = userCreation.UserName,
                Email = userCreation.Email,
                FirstName = userCreation.FirstName,
                LastName = userCreation.LastName,
                PhoneNumber = userCreation.PhoneNumber,
            };
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.DeleteAsync(user);
        }

        public async Task<UserVm> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found.");

            return new UserVm
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
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
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

        public async Task<PagingData<UserVm>> SearchUser(UserSearchRequest userSearch, PagingRequest pagingRequest)
        {
            var query = _userManager.Users.AsQueryable();
            if (pagingRequest.IsAscending)
            {
                query = query.OrderBy(p => EF.Property<object>(p, pagingRequest.SortBy));
            }
            else
            {
                query = query.OrderByDescending(p => EF.Property<object>(p, pagingRequest.SortBy));
            }

            if (!string.IsNullOrEmpty(userSearch.Name))
            {
                query = query.Where(u => 
                    (u.FirstName + " " + u.LastName).ToLower().Contains(userSearch.Name.ToLower())
                );
            }

            if (!string.IsNullOrEmpty(userSearch.Email))
            {
                query = query.Where(u => u.Email.ToLower().Contains(userSearch.Email.ToLower()));
            }

            var users = await query.ToListAsync();

            var userVms = new List<UserVm>();
            foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                userVms.Add(new UserVm
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = role
                });
            }

            return new PagingData<UserVm>(userVms, userVms.Count, pagingRequest.PageNumber, pagingRequest.PageSize);
        }
    }
}
