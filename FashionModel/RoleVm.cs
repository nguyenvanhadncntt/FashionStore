using System.ComponentModel.DataAnnotations;

namespace FashionStoreViewModel
{
    public class RoleVm
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
