using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Stripe;
using Stripe;
using System.Collections.Generic;

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
