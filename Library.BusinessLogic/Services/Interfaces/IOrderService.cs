using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Stripe;
using System.Collections.Generic;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<GetAllOrderViewItem> GetAll(string id);
        void Add(PayViewModel payViewModel);
    }
}
