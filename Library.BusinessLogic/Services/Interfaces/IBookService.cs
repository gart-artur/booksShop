using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Books.Get;
using Library.BusinessLogic.Services.ViewModel.Books.Post;
using Library.DataAccess.DTO;
using Library.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IBookService
    {
        GetAllBooksViewModelItem GetBooks();
        Book Get(int Id);
        void Insert(CreateBookViewModel createBookViewModel);
        void Delete(DeleteBooksViewModel deleteBooksViewModel);
        void Updata(EditBookViewModel editBookViewModel);

        Task<List<BookWithAuthorsDto>> SortByParamsAsync(FilterRequest request);
    }
}
