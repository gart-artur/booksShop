using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.DataAccess.Entities;

namespace Library.DataAccess.Interfaces
{
    public interface IBooksAdnAuthorRepository : IRepository<BooksAndAuthor>
    {
        BooksAndAuthor GetAuthorAndBooks(int AuthorId);
        BooksAndAuthor GetBookAndAuthors(int BookId);


    }
}
