using Microsoft.AspNetCore.Identity;

namespace Library.DataAccess.Entities
{
    public class User :IdentityUser
    { 
        public int Year { get; set; }
    }
}
