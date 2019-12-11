using AutoMapper;
using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Stripe;
using Library.DataAccess.Entities;
using System.Collections.Generic;

namespace Library.BusinessLogic.Configuration
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<IEnumerable<Order>, OrderViewModel>();
            CreateMap<Order, GetAllOrderViewItem>();
            CreateMap<GetAllOrderViewItem,Order>();
            CreateMap<PayViewModel, Order>()             
             .ForMember(x => x.Created, opt => opt.Ignore());
        }
    }
}
