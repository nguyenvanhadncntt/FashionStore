using System.ComponentModel.DataAnnotations;

namespace FashionStoreViewModel
{
    public class BrandVm
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
