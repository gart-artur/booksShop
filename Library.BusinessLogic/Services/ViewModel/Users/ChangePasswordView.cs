using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Services.ViewModel.Users
{
    public class ChangePasswordView
    {
        public string Email { get; set; }
        public string CurrentPassword{ get; set; }
        public string NewPassword { get; set; }
    }
}
