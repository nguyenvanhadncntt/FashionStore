using FashionStoreWebApi.Models;
using FashionStoreWebApi.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreWebApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<RoleVm> AddRoleAsync(string role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                Role newRole = new Role(role);

                await _roleManager.CreateAsync(newRole);

                return new RoleVm { Id = Guid.Parse(newRole.Id), Name = newRole.Name };
            } else
            {
                throw new Exception("Role already exists");
            }
        }

        public async Task<bool> deleteRole(string roleName)
        {
            Role? role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public Task<List<RoleVm>> GetRolesAsync()
        {
            return _roleManager.Roles.Select(r =>
                new RoleVm { Id = Guid.Parse(r.Id), Name = r.Name })
                .ToListAsync();
        }

        public async Task<RoleVm> UpdateRole(RoleVm roleVm)
        {
            Role? role = await _roleManager.FindByIdAsync(roleVm.Id.ToString());
            if (role == null)
            {
                throw new Exception("Role not found");
            }

            role.Name = roleVm.Name;

            await _roleManager.UpdateAsync(role);

            return new RoleVm { Id = Guid.Parse(role.Id), Name = role.Name };

        }
    }
}
