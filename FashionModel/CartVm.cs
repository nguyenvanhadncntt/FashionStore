using System.ComponentModel.DataAnnotations;

namespace FashionStoreViewModel
{
    public class CartVm
    {
        public long Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        [Required]
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImageUrl { get; set; }
        [Required]
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? AddedAt { get; set; }
    }
}
