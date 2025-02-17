using FashionStoreViewModel;

namespace FashionStoreWebApi.Services
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetRolesAsync();

        Task<RoleVm> UpdateRole(RoleVm roleVm);

        Task<RoleVm> AddRoleAsync(string role);

        Task<bool> deleteRole(string role);
    }
}
