using System.Collections.Generic;

namespace Library.DataAccess.DTO
{
    public class AuthorWithBooks
    {
        public int Total { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<AuthorWithBooksItem> Books { get; set; }
    }

    public class AuthorWithBooksItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }
    }
}
