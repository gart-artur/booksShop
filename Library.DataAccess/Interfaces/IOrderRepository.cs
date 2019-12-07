using Library.DataAccess.Entities;
using System.Collections.Generic;

namespace Library.DataAccess.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetAllOrdersByUserId(string id);
    }    
}
