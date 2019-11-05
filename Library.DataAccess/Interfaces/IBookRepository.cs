
using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccess.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> SortByParams(string name, int minPrice, int maxPrice);
    }

}
