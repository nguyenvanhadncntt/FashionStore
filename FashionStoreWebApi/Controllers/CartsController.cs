using System.Security.Claims;
using FashionStoreViewModel;
using FashionStoreWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartVm model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get the UserId of the currently logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Retrieves the UserId from claims
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User is not authenticated.");

            var cartItem = await _cartService.AddToCartAsync(userId, model.ProductId, model.Quantity);
            return Ok(cartItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItems()
        {
            // Get the UserId of the currently logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User is not authenticated.");

            return Ok(await _cartService.GetCartItemsByUserId(userId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItemQuantity([FromBody] CartVm model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _cartService.UpdateCartItemQuantityAsync(model.Id, model.Quantity);
            if (!result)
                return NotFound("Cart item not found.");

            return NoContent();
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            return Ok(await _cartService.RemoveFromCartAsync(cartItemId));
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            // Get the UserId of the currently logged-in user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User is not authenticated.");

            return Ok(await _cartService.ClearCartAsync(userId));
        }
    }
}
