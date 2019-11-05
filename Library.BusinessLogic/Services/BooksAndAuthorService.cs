using Library.BusinessLogic.Services.Interfaces;
using Library.DataAccess.Entities;
using Library.DataAccess.Repository;

namespace Library.BusinessLogic.Services
{
    public class BooksAndAuthorService : IBooksAndAuthorService
    {
        private BooksAndAuthorRepository _bookAndAuthorRepository;

        public BooksAndAuthorService(BooksAndAuthorRepository booksAndAuthorRepository)
        {
            _bookAndAuthorRepository = booksAndAuthorRepository;
        }
        public BooksAndAuthor GetAuthorAndBooks(int AuthorId)
        {
            return _bookAndAuthorRepository.GetAuthorAndBooks(AuthorId);
        }
        public BooksAndAuthor GetBookAndAuthors(int BookId)
        {
            return _bookAndAuthorRepository.GetBookAndAuthors(BookId);
        }
    }
}
