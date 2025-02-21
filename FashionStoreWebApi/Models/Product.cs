using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace FashionStoreWebApi.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public IList<Cart> CartItems { get; set; }

        [ConcurrencyCheck]
        public Guid Version { get; set; }
    }
}
