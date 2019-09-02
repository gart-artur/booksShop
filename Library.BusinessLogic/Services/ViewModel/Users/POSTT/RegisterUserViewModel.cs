using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.BusinessLogic.Services.ViewModel.Users.POSTT
{
    public class RegisterUserViewModel
    {
        public string Login { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Token { get; set; }
    }
}
