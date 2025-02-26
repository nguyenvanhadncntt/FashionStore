using System.ComponentModel.DataAnnotations;

namespace FashionStoreViewModel
{
    public class RoleVm
    {
        public string? Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
