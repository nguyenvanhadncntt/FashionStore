using System.ComponentModel.DataAnnotations;
using FashionStoreViewModel.Enumerations;

namespace FashionStoreViewModel
{
    public class OrderVm
    {
        public long Id { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        [Required]
        public string ContactPhoneNumber { get; set; }
        [Required]
        public string ContactName { get; set; }
        public string? ContactEmail { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        public string? OrderNote { get; set; }
        public DateTime? CreatedAt { get; set; }
        [Required]
        public IList<OrderItemVm> OrderItems { get; set; }
    }
}
