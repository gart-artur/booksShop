using System;

namespace Library.BusinessLogic.Services.ViewModel.Stripe
{
    public class PayViewModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public long Total { get; set; }
        public string BookName { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string BalanceTransactionId { get; set; }
    }
}
