using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Library.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        protected DbSet<Order> _entities;
        public OrderRepository(MagazineContext context) : base(context)
        {
            _entities = context.Set<Order>();
        }

        public IEnumerable<Order> GetAllOrdersByUserId(string id)
        {
            var listOfOrders = _entities.Where(e => e.UserId.Contains(id));
            return listOfOrders.ToList();
        }
    }
}
