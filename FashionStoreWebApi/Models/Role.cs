using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FashionStoreWebApi.Models
{
    [NotMapped]
    public class Role : IdentityRole
    {
        public Role() { }
        public Role(string roleName) : base(roleName)
        {
        }
    }
}
