using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> searchUser(
            [FromQuery] UserSearchRequest userSearchRequest, [FromQuery] PagingRequest pageRequest)
        {
            return Ok(await _userService.SearchUser(userSearchRequest, pageRequest));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            return Ok(await _userService.GetUserById(userId));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDTO userCreation)
        {
            return Ok(await _userService.CreateUserAsync(userCreation));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserCreationDTO userCreation)
        {
            return Ok(await _userService.CreateUserAsync(userCreation));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }


    }
}
