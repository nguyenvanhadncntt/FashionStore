﻿using FashionStoreWebApi.Models.DTOs;
using FashionStoreWebApi.Models;

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
    }
}
