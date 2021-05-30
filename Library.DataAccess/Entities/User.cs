using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Library.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public int Year { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
