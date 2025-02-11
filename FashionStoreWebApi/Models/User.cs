using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace FashionStoreWebApi.Models
{
    [NotMapped]
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public User(string userName) : base(userName)
        {
        }

        [PersonalData]
        public string FullName { get; set; }
        public IList<Order> Orders { get; set; }
        public IList<Cart> CartItems { get; set; }
    }
}
