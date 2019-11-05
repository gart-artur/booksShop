using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;

namespace Library.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MagazineContext context) : base(context)
        {
        }
    }
}
