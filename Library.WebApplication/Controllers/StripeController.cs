using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

using Stripe;

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
            _stripeService.PayOrder(model);  
             return Ok(); 
        }

        [HttpGet("order")]
        public IActionResult Order()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }
    }
}