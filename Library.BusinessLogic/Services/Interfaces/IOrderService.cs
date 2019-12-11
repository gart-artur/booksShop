using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderViewModel> GetAll(string id);
        void Add(PayViewModel payViewModel);
    }
}
