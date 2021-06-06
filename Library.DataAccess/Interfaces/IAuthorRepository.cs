using Library.DataAccess.DTO;
using Library.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.DataAccess.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<List<AuthorWithBooks>> GetWithBooks(int take, int skip);
    }
}
