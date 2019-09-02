using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Users.GET;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.WebApplication.Controllers
{   [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserViewModel param)
        {
            var user = _userService.Authenticate(param.Login, param.Password);
            if (user ==null)
            {
                return BadRequest(new { messate = "Error. Login or password is incorrect" });
            }
            return Ok(user);

        }

        [HttpGet("users")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }         
    }
}
