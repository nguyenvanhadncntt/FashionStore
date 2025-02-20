using System.ComponentModel.DataAnnotations;

namespace FashionStoreWebApi.Models.DTOs
{
    public class ProductCreationDTO
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public long BrandId { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public IFormFile? Image { get; set; }
    }
}
