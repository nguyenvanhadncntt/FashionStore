using System.Security.Claims;
using FashionStoreViewModel;
using FashionStoreWebApi.Models;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public UsersController(SignInManager<User> signInManager, IUserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> searchUser(
            [FromQuery] UserSearchRequest userSearchRequest, [FromQuery] PagingRequest pageRequest)
        {
            return Ok(await _userService.SearchUser(userSearchRequest, pageRequest));
        }

        [Authorize(Roles = "Admin")]
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
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserCreationDTO userCreation)
        {
            return Ok(await _userService.UpdateUserAsync(userCreation));
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

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] object empty)
        {
            if (empty is not null)
            {
                await _signInManager.SignOutAsync();
            }
            return Ok();
        }

        [Authorize]
        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _userService.GetUserById(userId));
        }
    }
}
