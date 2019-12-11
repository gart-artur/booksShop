using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("Authors")]
    public class Author : BaseEntity
    {
        public List<BooksAndAuthor> BooksAndAuthors { get; set; }
    }
}
