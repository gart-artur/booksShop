using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.BusinessLogic.Services.Interfaces;
using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Authors.Get;
using Library.BusinessLogic.Services.ViewModel.Authors.Post;
using Library.DataAccess.DTO;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;

namespace Library.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private IRepository<Author> _authorRepository;
        private IMapper _mapper;
        private readonly IAuthorRepository _authorRepositoryDapper;
        public AuthorService(
            IRepository<Author> authorRepository,
            IMapper mapper,
            IAuthorRepository authorRepositoryDapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _authorRepositoryDapper = authorRepositoryDapper;
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
        public async Task<List<AuthorWithBooks>> GetByFilter(FilterRequest request)
        {
            List<AuthorWithBooks> result = await _authorRepositoryDapper.GetWithBooks(request.Take, request.Skip);
            return result;
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
