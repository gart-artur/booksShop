using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.DataAccess.Entities
{
    public class User : BaseEntity
    {
        
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }


    }
}
