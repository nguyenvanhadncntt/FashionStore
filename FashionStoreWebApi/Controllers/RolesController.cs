using System.Security.Claims;
using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> AddRole(String role)
        {
            return Ok(await _roleService.AddRoleAsync(role));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] RoleVm roleVm)
        {
            return Ok(await _roleService.UpdateRole(roleVm));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole(String role)
        {
            return Ok(await _roleService.deleteRole(role));
        }
    }
}
