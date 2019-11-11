using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Helper
{
    public class SmtpOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string AuthEmail { get; set; }
        public string Password { get; set;}
        public string FromEmail { get; set;}       
    
    }
}
