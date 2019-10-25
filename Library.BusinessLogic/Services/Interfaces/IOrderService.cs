using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.ViewModel;
using System.Collections.Generic;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<GetAllOrderViewItem> GetAll();
        void Add(PayViewModel payViewModel);

    }
}
