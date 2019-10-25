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
        public StripeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("charge")]
        public IActionResult Charge([FromBody]PayViewModel payViewModel)
        {
            var i = payViewModel.BookName.Length - 1;
            payViewModel.BookName = payViewModel.BookName.Substring(0, i);
            payViewModel.Total *= 100;
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = payViewModel.Email,
                Source = payViewModel.Token,                
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = payViewModel.Total,
                Description = "Test customer for artur.hart@nure.ua",
                Currency="usd",
                Customer = customer.Id,
                ReceiptEmail = payViewModel.Email,
                Metadata = new Dictionary<string, string>()
                {
                    { "OrderId", "111"},
                    {"Postcode","LEE111" },
                }

            });
            if (charge.Status == "succeeded")
            {
                payViewModel.Total /= 100;
                payViewModel.Status = charge.Status;
                payViewModel.Created = charge.Created;
                payViewModel.BalanceTransactionId = charge.BalanceTransactionId;
                _orderService.Add(payViewModel);
                return Ok();
            }

            else{
                string message = "Sorry, something wrong...";
            }

            return View();
        }
        [HttpGet("order")]
        public IActionResult Order()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }
    }
}