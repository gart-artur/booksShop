using AutoMapper;
using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Stripe;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<OrderViewModel> GetAll(string id)
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllOrdersByUserId(id);
            var model = new OrderViewModel()
            {
                Orders = _mapper.Map<IEnumerable<Order>, List<GetAllOrderViewItem>>(orders)
            };
            return model;
        }
    }
}
