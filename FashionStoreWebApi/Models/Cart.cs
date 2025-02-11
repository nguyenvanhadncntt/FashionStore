namespace FashionStoreWebApi.Models
{
    public class Cart
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long ProductId { get; set; }
        public long Quantity { get; set; }
        public DateTime AddedAt { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
