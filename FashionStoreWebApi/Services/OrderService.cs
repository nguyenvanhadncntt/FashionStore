using FashionStoreWebApi.Data;
using FashionStoreWebApi.Helpers;
using FashionStoreWebApi.Models;
using FashionStoreViewModel;
using FashionStoreViewModel.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace FashionStoreWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly FashionStoreDbContext _context;

        public OrderService(FashionStoreDbContext context)
        {
            _context = context;
        }

        public async Task PlaceOrderAsync(string userId, OrderVm orderVm)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var productIds = orderVm.OrderItems.Select(oi => oi.ProductId).ToList();

                var products = await _context.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToListAsync();

                // Validate products quantity
                ValidateProductsQuantity(orderVm, products);

                // Create the order
                var order = new Order
                {
                    UserId = userId,
                    TotalAmount = orderVm.TotalAmount,
                    Status = OrderStatus.Created,
                    PaymentMethod = PaymentMethod.COD,
                    PaymentStatus = PaymentStatus.PENDING,
                    ShippingAddress = orderVm.ShippingAddress,
                    ContactName = orderVm.ContactName,
                    ContactPhoneNumber = orderVm.ContactPhoneNumber,
                    ContactEmail = orderVm.ContactEmail,
                    OrderNote = orderVm.OrderNote,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Add order items
                foreach (var orderItemVm in orderVm.OrderItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = orderItemVm.ProductId,
                        Quantity = orderItemVm.Quantity,
                        Price = orderItemVm.Price
                    };

                    _context.OrderItems.Add(orderItem);

                    // Update product stock
                    var product = products.FirstOrDefault(p => p.Id == orderItemVm.ProductId);
                    product.StockQuantity -= orderItemVm.Quantity;
                    _context.Products.Update(product);
                }

                // Remove carts
                var cartIds = orderVm.OrderItems.Select(oi => oi.cartId).ToList();
                await _context.Carts
                    .Where(c => cartIds.Contains(c.Id)).ExecuteDeleteAsync();

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    foreach (var entry in ex.Entries)
            //    {
            //        entry.Reload(); // Reload the entity from the database
            //    }
            //}
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private void ValidateProductsQuantity(OrderVm orderVm, IList<Product> products)
        {

            foreach (var orderItemVm in orderVm.OrderItems)
            {
                var product = products.FirstOrDefault(p => p.Id == orderItemVm.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with id {orderItemVm.ProductId} not found");
                }
                if (product.StockQuantity < orderItemVm.Quantity)
                {
                    throw new Exception($"Product {product.Name} is out of stock");
                }
            }
        }

        public async Task<OrderVm> GetOrderDetailAsync(long orderId)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            return ConvertVmHelper.convertToOrderVm(order);
        }

        public async Task<IList<OrderVm>> GetMyOrdersAsync(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();
            return orders.Select(o => ConvertVmHelper.convertToOrderVm(o)).ToList();
        }

        public async Task UpdateOrderStatusAsync(long orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new Exception($"Order with id {orderId} not found");
            }
            order.Status = status;
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentStatusAsync(long orderId, PaymentStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new Exception($"Order with id {orderId} not found");
            }
            order.PaymentStatus = status;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(long orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new Exception($"Order with id {orderId} not found");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<PagingData<OrderVm>> GetOrdersAsync(string name, PagingRequest pagingRequest)
        {
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.ContactName.ToLower().Contains(name.ToLower()));
            }

            var orderVms = await query.Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Select(o => ConvertVmHelper.convertToOrderVm(o))
                .ToListAsync();
            return new PagingData<OrderVm>(ConvertVmHelper.ExtractItemsPaging(orderVms, pagingRequest.PageNumber, pagingRequest.PageSize), 
                orderVms.Count, pagingRequest.PageNumber, pagingRequest.PageSize);
        }

        
    }
}
