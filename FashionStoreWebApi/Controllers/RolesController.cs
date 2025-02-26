using System.Security.Claims;
using FashionStoreViewModel;
using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _roleService.GetRolesAsync());
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoles(string roleId)
        {
            return Ok(await _roleService.GetRoleVmById(roleId));
        }

        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleVm role)
        {
            return Ok(await _roleService.AddRoleAsync(role.Name));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleVm roleVm)
        {
            return Ok(await _roleService.UpdateRole(roleVm));
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(String roleId)
        {
            return Ok(await _roleService.deleteRole(roleId));
        }
    }
}
