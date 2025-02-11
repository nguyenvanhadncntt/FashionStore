namespace FashionStoreWebApi.Models
{
    public class Order
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Pending/Completed/Cancelled
        public string PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
    }
}
