using System.ComponentModel.DataAnnotations;

namespace FashionStoreViewModel
{
    public class CartVm
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        [Required]
        public long Quantity { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
