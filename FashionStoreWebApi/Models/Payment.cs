namespace FashionStoreWebApi.Models
{
    public class Payment
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } // Success/Failure/Pending
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public Order Order { get; set; }
    }
}
