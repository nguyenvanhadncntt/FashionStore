using FashionStoreViewModel.Enumerations;

namespace FashionStoreWebApi.Models
{
    public class Order
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } // Pending/Completed/Cancelled
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }
}
