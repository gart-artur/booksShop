

using System.Collections.Generic;

namespace Library.DataAccess.Entities

{
    public class Author : BaseEntity
    {
        
        public string Name { get; set; }

        public List<BooksAndAuthor> BooksAndAuthors { get; set; }

    }
}
