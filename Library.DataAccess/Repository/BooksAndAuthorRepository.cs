using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.DataAccess.Repository
{
    public class BooksAndAuthorRepository : Repository<BooksAndAuthor>, IBooksAdnAuthorRepository
    {
        private readonly MagazineContext context;
        private DbSet<BooksAndAuthor> entities;
        public BooksAndAuthorRepository(MagazineContext context) : base(context)
        {
            this.context = context;
            entities = context.Set<BooksAndAuthor>();
        }

        public BooksAndAuthor GetAuthorAndBooks(int AuthorId)
        {
            return entities.SingleOrDefault(s => s.Id == AuthorId);
        }

        public BooksAndAuthor GetBookAndAuthors(int BookId)
        {
            return entities.SingleOrDefault(s => s.Id == BookId);
        }
    }
}
