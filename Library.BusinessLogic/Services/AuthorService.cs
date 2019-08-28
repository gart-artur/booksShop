using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel.Authors.Get;
using Library.BusinessLogic.Services.ViewModel.Authors.Post;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;

namespace Library.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private IRepository<Author> _authorRepository;
        private IMapper _mapper;
        
        public AuthorService(IRepository<Author> authorRepository,IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public void Delete(DeleteAuthorsViewModel deleteAuthorsViewModel)
        {
            Author author = new Author();
            author.Id = deleteAuthorsViewModel.Id;

            _authorRepository.Delete(author);
          
        }

        public Author Get(int Id)
        {
            return _authorRepository.Get(Id);
        }

        public GetAllAuthorViewModelItem GetAuthors()
        {

            IEnumerable <Author> authors= _authorRepository.GetAll();
            var authorView = new GetAllAuthorViewModelItem();
            authorView.Authors = _mapper.Map<IEnumerable<Author>, List<GetAllAuthorViewModelItem>>(authors);
            return authorView;

        }


        public void Insert(CreateAuthorViewModel createAuthorViewModel)
        {
            Author author = _mapper.Map<CreateAuthorViewModel, Author>(createAuthorViewModel);
            _authorRepository.Insert(author);

        }

        public void Updata(Author author)
        {
            _authorRepository.Update(author);
        }
    }
}
