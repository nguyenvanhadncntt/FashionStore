using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Models;
using FashionStoreViewModel;

namespace FashionStoreWebApi.Helpers
{
    public class ConvertVmHelper
    {
        public static BrandVm ConvertToBrandVm(Brand? brand)
        {
            return new BrandVm
            {
                Id = brand.Id,
                Name = brand.Name,
                Description = brand.Description
            };
        }

        public static CategoryVm ConvertToCategoryVm(Category? category)
        {
            return new CategoryVm
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static OrderVm convertToOrderVm(Order? order)
        {
            return new OrderVm()
            {
                Id = order.Id,
                OrderItems = order.OrderItems.Select(oi => new OrderItemVm
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    ProductImageUrl = oi.Product.ImageUrl,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList(),
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                ShippingAddress = order.ShippingAddress,
                ContactName = order.ContactName,
                ContactPhoneNumber = order.ContactPhoneNumber,
                ContactEmail = order.ContactEmail,
                OrderNote = order.OrderNote,
                CreatedAt = order.CreatedAt
            };
        }

        public static ProductVm ConvertToProductVm(Product p)
        {
            return new ProductVm
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                CategoryName = p.Category.Name,
                BrandName = p.Brand.Name
            };
        }

        public static User ConvertToUserEntity(UserCreationDTO userCreation)
        {
            // Create the user
            return new User
            {
                UserName = userCreation.Email,
                Email = userCreation.Email,
                FirstName = userCreation.FirstName,
                LastName = userCreation.LastName,
                PhoneNumber = userCreation.PhoneNumber,
            };
        }

        public static IList<T> ExtractItemsPaging<T>(IList<T> items, int page, int pageSize)
        {
            return items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
