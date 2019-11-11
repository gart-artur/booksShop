using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Helper
{
    public class JwtOptions
    {
        public string JwtKey { get; set; }   
        public string JwtIssuer { get; set; }
        public int JwtExpireDays { get; set; }
    }
}
