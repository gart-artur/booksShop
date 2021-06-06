using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.DTO;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;

namespace Library.DataAccess.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
         public AuthorRepository(MagazineContext context) : base(context)
         {   
         }

        public Task<List<AuthorWithBooks>> GetWithBooks(int take, int skip)
        {
            throw new NotImplementedException();
        }
    }
}

      
