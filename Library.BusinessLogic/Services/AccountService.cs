using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Users;
using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Library.BusinessLogic.Helper;
using MimeKit;
using MailKit.Net.Smtp;
using System.Web;
using PasswordGenerator;
using Microsoft.Extensions.Options;

namespace Library.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        private readonly IOptions<JwtOptions> _jwtOption;
        private readonly IOptions<SmtpOptions> _SmtpOptions;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IOptions<JwtOptions> jwtOption, IOptions<SmtpOptions> SmtpOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtOption = jwtOption;
            _SmtpOptions = SmtpOptions;
        }
        public async Task<User> RegisterUser(RegisterViewModel model)
        {
            User user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                NormalizedUserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                string themeOfMessage = "Confirm your Email";
                string nameActionOnController = "confirm";
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string encodedCode = HttpUtility.UrlEncode(code);
                await SendEmailAsync(user.Email, encodedCode, user.Id, themeOfMessage, nameActionOnController);
                await _signInManager.SignInAsync(user, false);
            }
            return user;
        }
        public async Task<JwtView> Login(LoginViewModel model)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                User appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                JwtView jwtModel = await GenerateJwtToken(model.Email, appUser);
                return jwtModel;
            }
            throw new ApplicationException("Incorrect login or password. User : " + model.Email);
        }
        private async Task<JwtView> GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Value.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtOption.Value.JwtExpireDays));

            var token = new JwtSecurityToken(
                _jwtOption.Value.JwtIssuer,
                _jwtOption.Value.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var encodedAccess = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtView
            {
                UserId = user.Id,
                Email = user.Email,
                AccessToken = encodedAccess,
            };
        }
        public async Task SendEmailAsync(string email, string encodedCode, string userId, string message, string nameActionOnController)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Site administration", _SmtpOptions.Value.FromEmail));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = message;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $"https://localhost:44348/api/Account/{nameActionOnController}?userId={userId}&code={encodedCode}"

                };
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_SmtpOptions.Value.Host, _SmtpOptions.Value.Port, false);
                    await client.AuthenticateAsync(_SmtpOptions.Value.AuthEmail, _SmtpOptions.Value.Password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
            }
        }
        public async Task SendNewPasswordOnEmail(string email, string message, string password)
        {
            await SendMail(email, message, password);
        }


        private async Task SendMail(string email, string message, string password)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Site administration", _SmtpOptions.Value.FromEmail));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = message;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"New password: {password}"

            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_SmtpOptions.Value.Host, _SmtpOptions.Value.Port, false);
                await client.AuthenticateAsync(_SmtpOptions.Value.AuthEmail, _SmtpOptions.Value.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }

        public async Task Confirm(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
        }
        public async Task ForgotPassword(ForgotPasswordView model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            bool isConfirmEmail = await _userManager.IsEmailConfirmedAsync(user);
            if (user != null || isConfirmEmail)
            {
                string themOfMessage = "Reset your password";
                string nameActionOnController = "reset";
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                string encodedCode = HttpUtility.UrlEncode(code);
                var userId = user.Id;
                await SendEmailAsync(user.Email, encodedCode, userId, themOfMessage, nameActionOnController);
            }
        }
        public async Task ResetPassword(string userId, string code)
        {
            //generate random new passwodr
            string message = "Your new password";
            var randomPassword = await GenerateRandomPassword();
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ResetPasswordAsync(user, code, randomPassword);
            SendNewPasswordOnEmail(user.Email, message, randomPassword);
        }
        public async Task<string> GenerateRandomPassword()
        {
            var pwd = new Password(12);
            string newPassword = pwd.ToString();
            return newPassword;
        }
        public async Task ChangeEmail(ChangeEmailView model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            bool isConfirmEmail = await _userManager.IsEmailConfirmedAsync(user);
            if (user != null || isConfirmEmail)
            {
                string themOfMessage = "Change your Email";
                string nameActionOnController = "AcceptNewEmail";
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
                string encodedCode = HttpUtility.UrlEncode(code);
                var userId = user.Id;
                await SendConfirmForChangeEmail(model.NewEmail, encodedCode, userId, themOfMessage, nameActionOnController);
            }

        }
        public async Task SendConfirmForChangeEmail(string newEmail, string encodedCode, string userId, string message, string nameActionOnController)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Site administration", _SmtpOptions.Value.FromEmail));
            emailMessage.To.Add(new MailboxAddress("", newEmail));
            emailMessage.Subject = message;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"https://localhost:44348/api/Account/{nameActionOnController}?userId={userId}&newEmail={newEmail}&code={encodedCode}"
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_SmtpOptions.Value.Host, _SmtpOptions.Value.Port, false);
                await client.AuthenticateAsync(_SmtpOptions.Value.AuthEmail, _SmtpOptions.Value.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);

            }
        }
        public async Task ResetEmail(string userId, string newEmail, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, code);
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordView model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            bool isCorrectPassword = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
            return result;
        }
    }
}

