using Library.BusinessLogic.Services.ViewModel;
using Library.BusinessLogic.Services.ViewModel.Authors.Get;
using Library.BusinessLogic.Services.ViewModel.Authors.Post;
using Library.DataAccess.DTO;
using Library.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService 
    {
        Task<List<AuthorWithBooks>> GetByFilter(FilterRequest request);

        Author Get(int Id);

        void Insert(CreateAuthorViewModel createAuthorViewModel);

        void Delete(DeleteAuthorsViewModel deleteAuthorsViewModel);

        void Updata(Author author);        
    }
}


