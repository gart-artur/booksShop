using Library.BusinessLogic.Services.ViewModel.Authors.Get;
using Library.BusinessLogic.Services.ViewModel.Authors.Post;

using Library.DataAccess.Entities;

namespace Library.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService 
    {
        GetAllAuthorViewModelItem GetAuthors();
        Author Get(int Id);
        void Insert(CreateAuthorViewModel createAuthorViewModel);
        void Delete(DeleteAuthorsViewModel deleteAuthorsViewModel);

        void Updata(Author author);

        
    }
}


