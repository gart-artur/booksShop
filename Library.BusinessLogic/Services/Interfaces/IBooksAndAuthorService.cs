using Library.DataAccess.Entities;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IBooksAndAuthorService
    {
        BooksAndAuthor GetBookAndAuthors(int bookId);
        BooksAndAuthor GetAuthorAndBooks(int authorId);
    }
}
