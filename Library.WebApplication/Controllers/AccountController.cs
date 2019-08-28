using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.Interfaces;
using Library.DataAccess.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Library.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        //[AllowAnonymous]
        //[HttpPost("Users")]
        //public IActionResult Index()
        //{
        //    var users = _userService.GetAll();
        //    return View(users);
        //}

        //[AllowAnonymous]
        [HttpGet("authenticate")]
        public IActionResult Authenticate(User user)
        {
            var userParam = _userService.Authenticate(user);

            //if (userParam == null)
            //    return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(userParam);
        }

        //[HttpGet("allUsers")]
        //public IActionResult GetAll()
        //{
        //    var users = _userService.GetAll();
        //    return Ok(users);
        //}
        [HttpGet("createGet")]
        public IActionResult Create()
        {
            return Ok();
        }
        [HttpPost("CratePost")]   
        public IActionResult Create(User user)
        {
            _userService.Create(user);
            return Ok(user);
        }
    }
}