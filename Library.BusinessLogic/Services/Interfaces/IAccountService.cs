using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.ViewModel.Users;
using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User> RegisterUser(RegisterViewModel model);
        Task<JwtView> Login(LoginViewModel model);
        Task Confirm(string userId, string code);
        Task ForgotPassword(ForgotPasswordView model);
        Task ResetPassword(string userId, string code);
        Task ChangeEmail(ChangeEmailView model);
        Task ResetEmail(string userId, string newEmail, string code);
        Task<IdentityResult> ChangePassword(ChangePasswordView model);
    }
}
