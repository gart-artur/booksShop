using AutoMapper;
using Library.BusinessLogic.Services.ViewModel.Authors.Get;
using Library.BusinessLogic.Services.ViewModel.Authors.Post;
using Library.DataAccess.Entities;

namespace Library.BusinessLogic.Configuration
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, GetAllAuthorViewModel>();
            CreateMap<CreateAuthorViewModel, Author>()
                .ForMember(x => x.BooksAndAuthors, opt => opt.Ignore())
                .ForMember(x => x.DateCreated, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<DeleteAuthorsViewModel, Author>();
        }
    }
}
