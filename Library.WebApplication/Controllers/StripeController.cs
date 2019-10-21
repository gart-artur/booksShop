using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.BusinessLogic.Helper;
using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace Library.WebApplication.Controllers
{
    [Route("api/[controller]")]
    public class StripeController : Controller
    {
        [HttpPost("charge")]
        public IActionResult Charge(PayViewModel payViewModel)
        {
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
                string BalanceTransactionId = charge.BalanceTransactionId;
                return Ok();
            }

            else{
                string message = "Sorry, something wrong....";
            }

            return View();
        }
    }
}