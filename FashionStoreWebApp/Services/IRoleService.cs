using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IRoleService
    {
        Task<FormResult> AddRoleAsync(string role);
        Task<FormResult> DeleteRole(string roleId);
        Task<List<RoleVm>> GetRolesAsync();
        Task<FormResult> UpdateRole(RoleVm roleVm);
        Task<RoleVm> GetRoleByIdAsync(string roleId);
    }
}
