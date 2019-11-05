using System.Collections.Generic;

namespace Library.DataAccess.Entities

{    public class Author : BaseEntity
    {
        public List<BooksAndAuthor> BooksAndAuthors { get; set; }
    }
}
