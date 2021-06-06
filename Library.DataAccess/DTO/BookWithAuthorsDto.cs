using System.Collections.Generic;

namespace Library.DataAccess.DTO
{
    public class BookWithAuthorsDto
    {
        public int Total { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public List<BookWithAuthorsDtoItems> Authors { get; set; }
    }

    public class BookWithAuthorsDtoItems
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
