using AutoMapper;
using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Stripe;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using System.Collections.Generic;

namespace Library.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private IMapper _mapper;        
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public void Add(PayViewModel payViewModel)
        {
            Order order = _mapper.Map<PayViewModel, Order>(payViewModel);
            _orderRepository.Insert(order);
        }
        public IEnumerable<GetAllOrderViewItem> GetAll()
        {
            var orders = _orderRepository.GetAll();            
            var returnedModel = _mapper.Map<IEnumerable<Order>,IEnumerable<GetAllOrderViewItem>>(orders);
            return returnedModel;
        }
    }
}
