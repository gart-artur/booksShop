using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("Books")]
    public class Book : BaseEntity
    {
        public int Price { get; set; } 

        [Key]
        public List<BooksAndAuthor> BooksAndAuthors { get; set; }
    }
}

