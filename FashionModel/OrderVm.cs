using FashionStoreViewModel.Enumerations;

namespace FashionStoreViewModel
{
    public class OrderVm
    {
        public long Id { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public IList<OrderItemVm> OrderItems { get; set; }
    }
}
