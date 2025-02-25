using FashionStoreWebApi.Data;
using FashionStoreWebApi.Models;
using FashionStoreViewModel;
using Microsoft.EntityFrameworkCore;
using FashionStoreWebApi.Exceptions;

namespace FashionStoreWebApi.Services
{
    public class CartService : ICartService
    {
        private readonly FashionStoreDbContext _context;

        public CartService(FashionStoreDbContext context)
        {
            _context = context;
        }
        public async Task<CartVm> AddToCartAsync(string userId, long productId, int quantity)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

            var existingCartItem = _context.Carts
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (existingCartItem != null)
            {
                if (existingCartItem.Quantity + quantity <= product.StockQuantity)
                {
                    existingCartItem.Quantity += quantity;
                    await _context.SaveChangesAsync();
                    return new CartVm()
                    {
                        UserId = userId,
                        ProductId = productId,
                        Quantity = existingCartItem.Quantity,
                        AddedAt = DateTime.UtcNow
                    };
                } else
                {
                    throw new QuantityOutOfStockException("Product quanlity out of stock!");
                }
            }

            var cartItem = new Cart
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                AddedAt = DateTime.UtcNow
            };

            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();
            return new CartVm()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = cartItem.Quantity,
                AddedAt = DateTime.UtcNow
            };
        }

        // Get all cart items for a user
        public async Task<IList<CartVm>> GetCartItemsByUserId(string userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .Include(c => c.User)
                .Select(c => new CartVm()
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    ProductImageUrl = c.Product.ImageUrl,
                    Price = c.Product.Price,
                    UserId = c.UserId,
                    UserName = c.User.FirstName + " " + c.User.LastName,
                    Quantity = c.Quantity,
                    AddedAt = c.AddedAt,
                })
                .ToListAsync();
        }

        // Update cart item quantity
        public async Task<bool> UpdateCartItemQuantityAsync(long cartItemId, int newQuantity)
        {
            var cartItem = await _context.Carts.FindAsync(cartItemId);

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);

            if (newQuantity <= product.StockQuantity)
            {
                cartItem.Quantity = newQuantity;
                return await _context.SaveChangesAsync() > 0;
            }
            else
            {
                throw new QuantityOutOfStockException("Product quanlity out of stock!");
            }
        }

        // Remove item from cart
        public async Task<bool> RemoveFromCartAsync(long cartItemId)
        {
            var cartItem = await _context.Carts.FindAsync(cartItemId);
            if (cartItem == null)
                return false;

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        // Clear all items from a user's cart
        public async Task<bool> ClearCartAsync(string userId)
        {
            var cartItems = _context.Carts.Where(c => c.UserId == userId).ToList();
            if (!cartItems.Any())
                return false;

            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
