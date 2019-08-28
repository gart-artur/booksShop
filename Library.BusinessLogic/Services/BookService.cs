﻿using System.Collections.Generic;
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


        public void Updata(Book book)
        {
            _bookRepository.Update(book);
        }


   
    }
}