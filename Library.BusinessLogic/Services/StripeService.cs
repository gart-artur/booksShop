using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Services
{
    public class StripeService : IStripeService
    {
        private readonly IOrderService _orderService;
        public StripeService(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public void PayOrder(PayViewModel payViewModel)
        {
/*            var i = payViewModel.BookName.Length - 1;
            payViewModel.BookName = payViewModel.BookName.Substring(0, i);*/
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
                Currency = "usd",
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
            }
            else
            {
                string message = "Sorry, something wrong...";
            }

        } 
    }
}
