using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.DataAccess.DTO;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        protected DbSet<Book> _entities;
        
        public BookRepository(MagazineContext context) : base(context)
        {
            _entities = context.Set<Book>();
        }

        public IEnumerable<Book> SortByParams(string name,int minPrice,int maxPrice)
        {
            var listOfBooks = _entities.Where(e => e.Name.Contains(name.ToLower()) && e.Price >= minPrice && e.Price <= maxPrice);
            return listOfBooks.ToList<Book>();
        }

        Task<List<BookWithAuthorsDto>> IBookRepository.GetWithAuthorsByFilter(int skip, int take)
        {
            throw new System.NotImplementedException();
        }
    }
}
