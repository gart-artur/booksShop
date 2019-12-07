using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Helper
{
    public class JwtView
    {
        public string UserId { get; set; }
        public string Email { get; set; }       
        public string AccessToken { get; set; }
    }
}
