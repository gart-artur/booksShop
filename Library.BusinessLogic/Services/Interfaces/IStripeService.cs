﻿using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.ViewModel.Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IStripeService
    {
        void PayOrder(PayViewModel payViewModel);
    }
}
