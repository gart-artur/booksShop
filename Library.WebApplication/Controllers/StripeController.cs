using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Stripe;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Library.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class StripeController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IStripeService _stripeService;
        public StripeController(IOrderService orderService, IStripeService stripeService)
        {
            _orderService = orderService;
            _stripeService = stripeService;
        }

        [HttpPost("charge")]
        public IActionResult Charge([FromBody]PayViewModel model)
        {
            model.Email = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub).Value;
            model.UserId= HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _stripeService.PayOrder(model);  
             return Ok(); 
        }

        [HttpGet("order")]
        public IActionResult Order()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orders = _orderService.GetAll(id);
            return Ok(orders);
        }
    }
}