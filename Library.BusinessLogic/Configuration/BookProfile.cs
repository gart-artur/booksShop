using AutoMapper;
using Library.BusinessLogic.Services.ViewModel.Books.Get;
using Library.BusinessLogic.Services.ViewModel.Books.Post;
using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Configuration
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, GetAllBooksViewModelItem>();
            CreateMap<CreateBookViewModel, Book>()
                .ForMember(x => x.BooksAndAuthors, opt => opt.Ignore())
                .ForMember(x => x.Id , opt => opt.Ignore());
            CreateMap<DeleteBooksViewModel, Book>()
                .ForMember(x => x.BooksAndAuthors, opt => opt.Ignore())
                .ForMember(x => x.DateModified, opt => opt.Ignore())
                 .ForMember(x => x.DateCreated, opt => opt.Ignore());


        }
    }
}
