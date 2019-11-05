using System.Collections.Generic;
using AutoMapper;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Books.Get;
using Library.BusinessLogic.Services.ViewModel.Books.Post;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;

namespace Library.BusinessLogic.Services
{
    public class BookService :  IBookService 
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        } 
        public void Delete(DeleteBooksViewModel deleteBooksViewModel)
        {
            var book = new Book();
            book.Id = deleteBooksViewModel.Id;
            _bookRepository.Delete(book);
        }
        public Book Get(int Id)
        {             
            return _bookRepository.Get(Id);
        } 
        public GetAllBooksViewModelItem GetBooks()
        {
            IEnumerable<Book> books = _bookRepository.GetAll(); 
            var bookView = new GetAllBooksViewModelItem();
            bookView.Books = _mapper.Map<IEnumerable<Book>, List<GetAllBooksViewModelItem>>(books); 
            return bookView;
        }
        public void Insert(CreateBookViewModel createBookViewModel)
        {           
            Book book = _mapper.Map<CreateBookViewModel, Book>(createBookViewModel);
            _bookRepository.Insert(book);        
        }
        public void Updata(EditBookViewModel editBookViewModel)
        {
            Book book = _mapper.Map<EditBookViewModel, Book>(editBookViewModel);
            _bookRepository.Update(book);            
        }   
        public FilterBooksListView SortByParams(SortBooksByParamsView model)
        {
           var booksView = new FilterBooksListView();            
                var chekName = string.IsNullOrEmpty(model.Name) ? "" : model.Name;
                var chekMaxPrice = model.MaxPrice > 0 ? model.MaxPrice : int.MaxValue;
            if (!(model.MinPrice > chekMaxPrice))
            {
                booksView.Books = _bookRepository.SortByParams(chekName, model.MinPrice, chekMaxPrice);
            }
            return booksView;
        }
    }
}