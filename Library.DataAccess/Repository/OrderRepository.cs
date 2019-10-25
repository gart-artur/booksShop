using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MagazineContext context) : base(context)
        {

        }
    }
}
