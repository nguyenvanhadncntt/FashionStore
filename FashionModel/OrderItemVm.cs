namespace FashionStoreViewModel
{
    public class OrderItemVm
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public long cartId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImageUrl { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
