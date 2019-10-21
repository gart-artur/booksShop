using Library.BusinessLogic.Services.ViewModel.Books.Get;
using Library.BusinessLogic.Services.ViewModel.Books.Post;
using Library.DataAccess.Entities;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IBookService
    {
        GetAllBooksViewModelItem GetBooks();
        Book Get(int Id);
        void Insert(CreateBookViewModel createBookViewModel);
        void Delete(DeleteBooksViewModel deleteBooksViewModel);
        void Updata(EditBookViewModel editBookViewModel);
        void AddToBucket(ShoppingCart shoppingCart);
    }
}
