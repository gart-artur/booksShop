using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            var result = await _accountService.RegisterUser(model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            var result = await _accountService.Login(model);
            return Ok(result);
        }

        [HttpGet("confirm")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            await _accountService.Confirm(userId, (string)code);
            return Ok();
        }

        [HttpPost("forgot")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordView model)
        {
            await _accountService.ForgotPassword(model);
            return Ok();
        }

        [HttpGet("reset")]
        public async Task<IActionResult> ResetPasswodr(string userId, string code)
        {
            await _accountService.ResetPassword(userId, code);
            return Ok();
        }


    }
}