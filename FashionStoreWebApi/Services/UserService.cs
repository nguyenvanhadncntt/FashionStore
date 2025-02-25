using FashionStoreWebApi.Models;
using FashionStoreViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FashionStoreWebApi.Helpers;
using FashionStoreWebApi.Exceptions;

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
            User user = ConvertVmHelper.ConvertToUserEntity(userCreation);
            await _userManager.CreateAsync(user, userCreation.Password);

            // Check the role is existing
            if (!await _roleManager.RoleExistsAsync(userCreation.Role))
            {
                throw new EntityNotFoundException($"Role with name {userCreation.Role} not found");
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

        public async Task<UserVm> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new EntityNotFoundException("User", userId);

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

        public async Task<IdentityResult> UpdateUserAsync(UserCreationDTO userDto)
        {
            User? user = await _userManager.FindByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new EntityNotFoundException("User", userDto.Id);
            }
            user.UserName = userDto.Email;
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
                    throw new EntityNotFoundException($"Role with name {userDto.Role} not found");
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

            return new PagingData<UserVm>(ConvertVmHelper.ExtractItemsPaging(userVms, pagingRequest.PageNumber, pagingRequest.PageSize),
                userVms.Count, pagingRequest.PageNumber, pagingRequest.PageSize);
        }
    }
}
