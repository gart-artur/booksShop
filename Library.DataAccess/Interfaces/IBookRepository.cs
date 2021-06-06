
using Library.DataAccess.DTO;
using Library.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.DataAccess.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> SortByParams(string name, int minPrice, int maxPrice);

        Task<List<BookWithAuthorsDto>> GetWithAuthorsByFilter(int skip, int take);
    }

}
