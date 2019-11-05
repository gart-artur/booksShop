using AutoMapper;
using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.ViewModel;
using Library.DataAccess.Entities;

namespace Library.BusinessLogic.Configuration
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<Order, GetAllOrderViewItem>();
            CreateMap<GetAllOrderViewItem,Order>();
            CreateMap<PayViewModel, Order>()
             .ForMember(x => x.Id, opt => opt.Ignore())
             .ForMember(x => x.Created, opt => opt.Ignore());
        }
    }
}
