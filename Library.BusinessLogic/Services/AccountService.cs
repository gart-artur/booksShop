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
using System.Web.Http.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Library.BusinessLogic.Helper;
using MimeKit;
using MailKit.Net.Smtp;
using System.Web;

namespace Library.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        public readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public async Task<User> RegisterUser(RegisterViewModel model)
        {
            User user = new User { Email = model.Email, UserName = model.Email, NormalizedUserName = model.UserName };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                string themOfMessage = "Confirm your Email";
                string nameActionOnController = "confirm";
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string encodedCode = HttpUtility.UrlEncode(code);
                var userId = user.Id;
                await SendEmailAsync(user.Email, encodedCode, userId, themOfMessage, nameActionOnController);
                await _signInManager.SignInAsync(user, false);
            }
            return user;
        }

        public async Task<JwtView> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                JwtView jwtModel = await GenerateJwtToken(model.Email, appUser);
                return jwtModel;
            }
            throw new ApplicationException("INVALID LOGIN");
        }

        private async Task<JwtView> GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var encodedAccess = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtView
            {
                Email = user.Email,
                AccessToken = encodedAccess,
            };
        }

        public async Task SendEmailAsync(string email, string encodedCode, string userId, string message,string nameActionOnController)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Site administration", "artur.gart99@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = message;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $"https://localhost:44348/api/Account/{nameActionOnController}?userId={userId}&code={encodedCode}"

                };
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("artur.gart99@gmail.com", "z1x5c9v8");
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
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Site administration", "artur.gart99@gmail.com"));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = message;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = $"New password:{password}"

                };
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("artur.gart99@gmail.com", "z1x5c9v8");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
            }
        }

        public async Task Confirm(string userId, string code)
        {
            try {
                var user = await _userManager.FindByIdAsync(userId);
                var result = await _userManager.ConfirmEmailAsync(user, code);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
            }
        }

        public async Task ForgotPassword(ForgotPasswordView model)
        {
            try { 
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
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
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
        public async Task<string>  GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };
            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@?"                        // non-alphanumeric
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }
            var password = new string(chars.ToArray());
            return password;
        }

    }
}
