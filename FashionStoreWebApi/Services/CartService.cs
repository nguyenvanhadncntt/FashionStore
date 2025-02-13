using System.Security.Claims;
using FashionStoreWebApi.Data;
using FashionStoreWebApi.Models;
using Microsoft.AspNetCore.Identity;

namespace FashionStoreWebApi.Services
{
    public class CartService : ICartService
    {
        private readonly FashionStoreDbContext _context;

        public CartService(FashionStoreDbContext context)
        {
            _context = context;
        }
        //public async Task<Cart> AddToCartAsync(long productId, long quantity)
        //{
        //    var existingCartItem = _context.Carts
        //        .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

        //    if (existingCartItem != null)
        //    {
        //        // If the product already exists in the cart, update the quantity
        //        existingCartItem.Quantity += quantity;
        //        await _context.SaveChangesAsync();
        //        return existingCartItem;
        //    }

        //    // Otherwise, create a new cart item
        //    var cartItem = new Cart
        //    {
        //        UserId = userId,
        //        ProductId = productId,
        //        Quantity = quantity,
        //        AddedAt = DateTime.UtcNow
        //    };

        //    _context.Carts.Add(cartItem);
        //    await _context.SaveChangesAsync();
        //    return cartItem;
        //}

        //// Get all cart items for a user
        //public IEnumerable<Cart> GetCartItemsByUserId(int userId)
        //{
        //    return _context.Carts
        //        .Where(c => c.UserId == userId)
        //        .Include(c => c.Product) // Include product details
        //        .ToList();
        //}

        //// Update cart item quantity
        //public async Task<bool> UpdateCartItemQuantityAsync(int cartItemId, int newQuantity)
        //{
        //    var cartItem = await _context.Carts.FindAsync(cartItemId);
        //    if (cartItem == null)
        //        return false;

        //    cartItem.Quantity = newQuantity;
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //// Remove item from cart
        //public async Task<bool> RemoveFromCartAsync(int cartItemId)
        //{
        //    var cartItem = await _context.Carts.FindAsync(cartItemId);
        //    if (cartItem == null)
        //        return false;

        //    _context.Carts.Remove(cartItem);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //// Clear all items from a user's cart
        //public async Task<bool> ClearCartAsync(int userId)
        //{
        //    var cartItems = _context.Carts.Where(c => c.UserId == userId).ToList();
        //    if (!cartItems.Any())
        //        return false;

        //    _context.Carts.RemoveRange(cartItems);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

    }
}
