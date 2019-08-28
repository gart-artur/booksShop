using System.Collections.Generic;



namespace Library.DataAccess.Entities
{
    public class Book : BaseEntity
    {
        public int Price { get; set; }
       
        public string Name { get; set; }

        public List<BooksAndAuthor> BooksAndAuthors { get; set; }
    }
}

