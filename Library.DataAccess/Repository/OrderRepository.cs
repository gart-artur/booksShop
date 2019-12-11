using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        protected DbSet<Order> _entities;
        public OrderRepository(MagazineContext context) : base(context)
        {
            _entities = context.Set<Order>();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByUserId(string id)
        {
            IEnumerable<Order> orders = _entities.Where(e => e.UserId.Contains(id));
            return orders;
        }
    }
}
